using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Data;
using BarStockControl.Models;
using BarStockControl.Models.Enums;
using BarStockControl.Mappers;
using BarStockControl.DTOs;

namespace BarStockControl.Services
{
    public class BackupService : BaseService<Backup>
    {
        private readonly string _dataFilePath = "Xml/data.xml";
        private readonly string _backupFolder = "BackUps";
        private readonly UserService _userService;

        public BackupService(XmlDataManager dataManager, UserService userService)
    : base(dataManager, "backups")
        {
            _userService = userService;

            if (!Directory.Exists(_backupFolder))
                Directory.CreateDirectory(_backupFolder);
        }

        public List<BackupDto> GetAllDto()
        {
            try
            {
                var backups = GetAll();
                return backups.Select(BackupMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener backups: {ex.Message}", ex);
            }
        }

        public void CreateBackup(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    throw new ArgumentNullException(nameof(userDto), "El usuario no puede ser null.");

                var user = _userService.GetById(userDto.Id);
                if (user == null)
                    throw new InvalidOperationException("Usuario no encontrado.");

                if (!File.Exists(_dataFilePath))
                    throw new FileNotFoundException($"Archivo de datos no encontrado: {_dataFilePath}");

                var driveInfo = new DriveInfo(Path.GetPathRoot(_backupFolder));
                if (driveInfo.AvailableFreeSpace < 1024 * 1024)
                    throw new InvalidOperationException("Espacio insuficiente en disco para crear backup.");

                var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_backup.xml";
                var destination = Path.Combine(_backupFolder, fileName);
                File.Copy(_dataFilePath, destination, true);

                var backup = new Backup
                {
                    Id = GetNextId(),
                    Date = DateTime.Now,
                    Detail = BackupType.Backup,
                    User = user
                };
                Add(backup);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear backup: {ex.Message}", ex);
            }
        }

        public void RestoreBackup(string fileName, UserDto userDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                    throw new ArgumentException("El nombre del archivo no puede estar vacío.", nameof(fileName));

                if (userDto == null)
                    throw new ArgumentNullException(nameof(userDto), "El usuario no puede ser null.");

                var user = _userService.GetById(userDto.Id);
                if (user == null)
                    throw new InvalidOperationException("Usuario no encontrado.");

                var source = Path.Combine(_backupFolder, fileName);
                if (!File.Exists(source))
                    throw new FileNotFoundException($"Archivo de backup no encontrado: {fileName}");

                var backupContent = File.ReadAllText(source);
                if (!backupContent.Contains("<data>"))
                    throw new InvalidOperationException("El archivo de backup no tiene un formato válido.");

                var currentBackupFileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_pre_restore_backup.xml";
                var currentBackupPath = Path.Combine(_backupFolder, currentBackupFileName);
                if (File.Exists(_dataFilePath))
                {
                    File.Copy(_dataFilePath, currentBackupPath, true);
                }

                File.Copy(source, _dataFilePath, true);

                var restore = new Backup
                {
                    Id = GetNextId(),
                    Date = DateTime.Now,
                    Detail = BackupType.Restore,
                    User = user
                };
                Add(restore);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al restaurar backup: {ex.Message}", ex);
            }
        }

        public void DeleteBackupFile(string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                    throw new ArgumentException("El nombre del archivo no puede estar vacío.", nameof(fileName));

                var fullPath = Path.Combine(_backupFolder, fileName);
                if (!File.Exists(fullPath))
                    throw new FileNotFoundException($"Archivo de backup no encontrado: {fileName}");

                // Verificar que no estamos eliminando el archivo de datos principal
                if (Path.GetFullPath(fullPath).Equals(Path.GetFullPath(_dataFilePath), StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("No se puede eliminar el archivo de datos principal.");

                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar archivo de backup: {ex.Message}", ex);
            }
        }

        protected override Backup MapFromXml(XElement element)
        {
            return BackupMapper.FromXml(element, _userService.GetById);
        }

        protected override XElement MapToXml(Backup backup)
        {
            return BackupMapper.ToXml(backup);
        }
    }
}
