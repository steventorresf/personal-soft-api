using MongoDB.Driver;
using PersonalSoft.Entities;

namespace PersonalSoft.Persistence.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MongoDBContext _context;
        private const string UserAdmin = "admin";
        private const string PasswordAdmin = "12345";

        public UsuarioRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerUsuarioByLogin(string nombreUsuario, string password)
        {
            FilterDefinition<Usuario> filter = Builders<Usuario>.Filter.Eq(x => x.NombreUsuario, nombreUsuario);
            Usuario? user = await _context.UsuarioCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

            if (user == null)
                return null;

            bool userValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!userValid)
                return null;

            return user;
        }

        public async Task CreateUserAdmin()
        {
            FilterDefinition<Usuario> filter = Builders<Usuario>.Filter.Eq(x => x.NombreUsuario, UserAdmin);
            Usuario? admin = await _context.UsuarioCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
            if(admin == null)
            {
                admin = new()
                {
                    NombreCompleto = "Administrador del sistema",
                    NombreUsuario = UserAdmin,
                    Password = BCrypt.Net.BCrypt.HashPassword(PasswordAdmin),
                    Estado = true
                };
                await _context.UsuarioCollection.InsertOneAsync(admin);
            }
        }
    }
}
