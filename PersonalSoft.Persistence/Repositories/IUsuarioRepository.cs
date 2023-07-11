using PersonalSoft.Entities;

namespace PersonalSoft.Persistence.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerUsuarioByLogin(string nombreUsuario, string password);
        Task CreateUserAdmin();
    }
}
