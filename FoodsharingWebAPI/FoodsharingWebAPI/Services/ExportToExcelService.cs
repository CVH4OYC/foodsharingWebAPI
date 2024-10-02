using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using FoodsharingWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Services
{
    /// <summary>
    /// Сервис для экспорта сущностей из БД в Excel
    /// </summary>
    public class ExportToExcelService
    {
        private readonly IRepository<Address> addressRepository;
        private readonly IRepository<Announcement> announcementRepository;
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Chat> chatRepository;
        private readonly IRepository<Message> messageRepository;
        private readonly IRepository<MessageStatus> messageStatusRepository;
        private readonly IRepository<Organization> organizationRepository;
        private readonly IRepository<Profile> profileRepository;
        private readonly IRepository<Representative> representativeRepository;
        private readonly IRepository<Role> roleRepository;
        private readonly IRepository<Transaction> transactionRepository;
        private readonly IRepository<TransactionStatus> transactionStatusRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<UserRole> userRoleRepository;

        public ExportToExcelService(IRepository<Address> addressRepository, IRepository<Announcement> announcementRepository,
            IRepository<Category> categoryRepository, IRepository<Chat> chatRepository,
            IRepository<Message> messageRepository, IRepository<MessageStatus> messageStatusRepository,
            IRepository<Organization> organizationRepository, IRepository<Profile> profileRepository,
            IRepository<Representative> representativeRepository, IRepository<Role> roleRepository,
            IRepository<Transaction> transactionRepository, IRepository<TransactionStatus> transactionStatusRepository,
            IRepository<User> userRepository, IRepository<UserRole> userRoleRepository)
        {
            this.addressRepository = addressRepository;
            this.announcementRepository = announcementRepository;
            this.categoryRepository = categoryRepository;
            this.chatRepository = chatRepository;
            this.messageRepository = messageRepository;
            this.messageStatusRepository = messageStatusRepository;
            this.organizationRepository = organizationRepository;
            this.profileRepository = profileRepository;
            this.representativeRepository = representativeRepository;
            this.roleRepository = roleRepository;
            this.transactionRepository = transactionRepository;
            this.transactionStatusRepository = transactionStatusRepository;
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// Экспортирует все таблицы БД в файл Excel и возвращает файл в виде массива байтов
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Массив байтов, представляющий файл Excel</returns>
        public async Task<byte[]> ExportEntitiesToExcelAsync(CancellationToken cancellationToken=default)
        {
            using (var workbook = new XLWorkbook())
            {
                var addresses = await addressRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, addresses.ToList());

                var announcements = await announcementRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, announcements.ToList());

                var categories = await categoryRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, categories.ToList());

                var chats = await chatRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, chats.ToList());

                var messages = await messageRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, messages.ToList());

                var messageStatuses = await messageStatusRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, messageStatuses.ToList());

                var organizations = await organizationRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, organizations.ToList());

                var profiles = await profileRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, profiles.ToList());

                var representatives = await representativeRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, representatives.ToList());

                var roles = await roleRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, roles.ToList());

                var transactions = await transactionRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, transactions.ToList());

                var transactionStatuses = await transactionStatusRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, transactionStatuses.ToList());

                var users = await userRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, users.ToList());

                var userRoles = await userRoleRepository.GetAllAsync(cancellationToken);
                AddListToExcel(workbook, userRoles.ToList());

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
        /// <summary>
        /// Добавляет в книгу новый лист и записывает на него данные из списка типа сущности
        /// </summary>
        /// <typeparam name="T">Тип сущности, данные которой добавляются в Excel</typeparam>
        /// <param name="workbook">Книга Excel, в которую добавляются данные</param>
        /// <param name="data">Список сущностей, который нужно экспортировать</param>
        public void AddListToExcel<T>(XLWorkbook workbook, List<T> data)
        {
            var worksheet = workbook.Worksheets.Add(typeof(T).Name);
            var properties = typeof(T).GetProperties()
                .Where(p => p.PropertyType.IsPrimitive ||
                p.PropertyType == typeof(string) ||
                p.PropertyType == typeof(DateTime) ||
                p.PropertyType == typeof(decimal) ||
                p.PropertyType.IsValueType).ToArray();

            for (int i = 0; i < properties.Length; i++)
                worksheet.Cell(1, i + 1).Value = properties[i].Name;

            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < properties.Length; j++)
                {
                    var value = properties[j].GetValue(data[i]);
                    worksheet.Cell(i + 2, j + 1).Value = value?.ToString() ?? string.Empty;
                }
            }
        }
    }    
}