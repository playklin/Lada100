using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lada100.Models
{
    public class SpisoksRepository
    {
        private readonly AppDbContext context;

        public SpisoksRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void CreateSpisok(Spisok spisok)  // Создание нового пункта 
        {
            context.Spisoks.Add(spisok);
            context.SaveChanges();
        }
        public void DeleteSpisok(Spisok spisok)  // Удоление пункта из списка
        {
            context.Spisoks.Remove(spisok);
            context.SaveChanges();
        }

        public void EditSpisok(Spisok spisok) // Изменение данных в пункте
        {
            context.Spisoks.Update(spisok);
            context.SaveChanges();
            //context.SaveChangesAsync();
        }

        public List<Spisok> GetAllSpisok() // Получаем весь список
        {
            return context.Spisoks.ToList(); // можно добавить по возростанию (есть ошибка) return context.Spisoks.OrderBy(x => x.WorkTotal);

        }

        public Spisok GetSingleSpisok(int id) // Получаем один пункт из списка
        {
            var spisok = context.Spisoks.FirstOrDefault(p => p.Id == id);
            return spisok;
        }

        public List<Spisok> SearchSpisok(string search) // Осуществляем поиск в списке по введенным данным
        {
            int pricetotal;
            bool success = Int32.TryParse(search, out pricetotal);
            if (!success)
                pricetotal = 0;
            return context.Spisoks
                               .Where(p => p.WorkTotal.Contains(search) ||
                                          /*p.Spare.Contains(search) ||*/
                                          p.PriceTotal == pricetotal)
                               .ToList();

        }



    }
}
