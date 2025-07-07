using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Data;
using BarStockControl.Mappers;
using System.Text.RegularExpressions;
using BarStockControl.Security;
using BarStockControl.DTOs;


namespace BarStockControl.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "users") { }

        protected override User MapFromXml(XElement element)
        {
            return UserMapper.FromXml(element);
        }

        protected override XElement MapToXml(User user)
        {
            return UserMapper.ToXml(user);
        }

        public List<string> ValidateUser(User user, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(user.FirstName))
                errors.Add("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(user.LastName))
                errors.Add("El apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
                errors.Add("El email ingresado no es válido.");

            if (!isUpdate && GetAll().Any(u => u.Email == user.Email))
                errors.Add("Ya existe un usuario con ese email.");

            if (string.IsNullOrWhiteSpace(user.Password) || !Regex.IsMatch(user.Password, @"^[a-zA-Z0-9]{6,}$"))
            {
                errors.Add("La contraseña debe tener al menos 6 caracteres y solo puede contener letras y números.");
            }

            return errors;
        }

        public List<string> CreateUser(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    throw new ArgumentNullException(nameof(userDto), "El usuario no puede ser null.");

                var user = UserMapper.ToEntity(userDto);
                var errors = ValidateUser(user);
                if (errors.Any())
                    return errors;

                user.Id = GetNextId();
                Add(user);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear usuario: {ex.Message}", ex);
            }
        }

        public List<string> UpdateUser(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    throw new ArgumentNullException(nameof(userDto), "El usuario no puede ser null.");

                var user = UserMapper.ToEntity(userDto);
                var errors = ValidateUser(user, isUpdate: true);
                if (errors.Any())
                    return errors;

                Update(user.Id, user);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al actualizar usuario: {ex.Message}", ex);
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID del usuario debe ser mayor a 0.", nameof(id));

                var user = GetById(id);
                if (user == null)
                    throw new InvalidOperationException($"Usuario con ID {id} no encontrado.");

                Delete(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar usuario: {ex.Message}", ex);
            }
        }

        public User GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return null;

                return GetAll().FirstOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener usuario por ID: {ex.Message}", ex);
            }
        }

        public List<UserDto> GetAllUsers()
        {
            try
            {
                return GetAll().Select(UserMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener todos los usuarios: {ex.Message}", ex);
            }
        }

        public List<User> Search(Func<User, bool> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate), "El predicado de búsqueda no puede ser null.");

                return GetAll().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al buscar usuarios: {ex.Message}", ex);
            }
        }

        public string DecryptPassword(string encryptedPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(encryptedPassword))
                    return string.Empty;

                return PasswordEncryption.DecryptPassword(encryptedPassword);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al desencriptar contraseña: {ex.Message}", ex);
            }
        }

        public User Authenticate(string email, string plainPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("El email no puede estar vacío.", nameof(email));

                if (string.IsNullOrWhiteSpace(plainPassword))
                    throw new ArgumentException("La contraseña no puede estar vacía.", nameof(plainPassword));

                string encryptedPassword = PasswordEncryption.EncryptPassword(plainPassword);

                var user = GetAll().FirstOrDefault(u => 
                    u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && 
                    u.Password == encryptedPassword && 
                    u.Active);

                return user;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en autenticación: {ex.Message}", ex);
            }
        }

        public UserDto GetUserDtoById(int id)
        {
            var user = GetById(id);
            return UserMapper.ToDto(user);
        }
    }
}
