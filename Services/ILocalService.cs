using Parcial2.Models;

namespace Parcial2.Services;
public interface ILocalService
{
    void Create(Local obj);
    List<Local> GetAll();
    List<Local> GetAllLocales();
    void Update(Local obj);
    void Delete(Local obj);
    Local? GetById(int id);
}