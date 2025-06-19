using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProyectoPROGEND.Controllers;

public class BaseDatosController : Controller
{
    // Acción para mostrar la vista de respaldo 
    public IActionResult Backup()
    {
        return View();
    }

    // Acción para descargar el archivo de respaldo
    public async Task<IActionResult> BackupDatabase()
    {
        string backupFileName = "bdwebappfitness_backup.sql";

        // Ejecutar el comando mysqldump para crear el archivo de respaldo
        string mysqldumpPath = @"C:\xampp\mysql\bin\mysqldump.exe";
        string arguments = "--user=root --password= --default-character-set=utf8 --routines --triggers --skip-comments db-webfitness";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = mysqldumpPath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        string output;
        string error;

        using (Process process = new Process { StartInfo = psi })
        {
            process.Start();
            output = await process.StandardOutput.ReadToEndAsync();
            error = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();

            // Ignorar la advertencia sobre la contraseña
            if (error.Contains("[Warning] Using a password on the command line interface can be insecure."))
            {
                error = string.Empty;
            }

            if (!string.IsNullOrEmpty(error))
            {
                TempData["Message"] = $"Error al realizar el respaldo: {error}";
                return RedirectToAction("Backup");
            }
        }

        TempData["Message"] = "Respaldo realizado con éxito.";
        // Retornar el archivo para descargarlo sin guardarlo en el servidor
        return File(System.Text.Encoding.UTF8.GetBytes(output), "application/octet-stream", backupFileName);
    }

    /*// Acción para cargar el archivo de respaldo
    [HttpGet]
    public IActionResult Restore()
    {
        return View();
    }*/

    [HttpPost]
    public async Task<IActionResult> RestoreDatabase(IFormFile backupFile)
    {
        if (backupFile != null && backupFile.Length > 0)
        {
            // 1. Guardar el archivo de respaldo temporalmente
            string backupFilePath = Path.GetTempFileName();
            using (var stream = new FileStream(backupFilePath, FileMode.Create))
            {
                await backupFile.CopyToAsync(stream);
            }

            // 2. Validar que sea .sql
            if (Path.GetExtension(backupFile.FileName).ToLower() != ".sql")
            {
                TempData["Message"] = "El archivo proporcionado no es un archivo de respaldo válido.";
                return RedirectToAction("Backup");
            }

            // 3. Preparar el cmd con redirección '<'
            string mysqlPath = @"C:\xampp\mysql\bin\mysql.exe";

            // Ya no necesitamos safePath ni el comando -e source:
            // Creamos un ProcessStartInfo que invoque cmd.exe y use '< archivo.sql'
            var psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C \"\"{mysqlPath}\" --user=root --password= db-webfitness < \"{backupFilePath}\"\"",
                UseShellExecute = true,    // IMPORTANTE para que cmd.exe procese la redirección
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                // Esperamos hasta 30 segundos
                if (!process.WaitForExit(30000))
                {
                    process.Kill();
                    TempData["Message"] = "El proceso de restauración tomó demasiado tiempo y fue detenido.";
                    return RedirectToAction("Backup");
                }

                if (process.ExitCode != 0)
                {
                    TempData["Message"] = $"Error al restaurar la base de datos (código {process.ExitCode}).";
                    return RedirectToAction("Backup");
                }
            }

            TempData["Message"] = "Base de datos restaurada con éxito.";
            return RedirectToAction("Backup");
        }

        TempData["Message"] = "No se ha proporcionado un archivo de respaldo válido.";
        return RedirectToAction("Backup");
    }

}