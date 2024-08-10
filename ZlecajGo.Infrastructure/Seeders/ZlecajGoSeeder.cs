using Microsoft.AspNetCore.Identity;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Infrastructure.Persistence;
using Type = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.Infrastructure.Seeders;

internal class ZlecajGoSeeder(ZlecajGoContext dbContext, UserManager<User> userManager) : IZlecajGoSeeder
{
    public async Task SeedAsync(bool enabled = true)
    {
        if (enabled is false || !await dbContext.Database.CanConnectAsync()) return;
        
        #region Categories, Statuses, Types
        
            var electrician = new Category { Name = "Elektryk" };
            var handyman = new Category { Name = "Złota rączka" };
            var furnitureAssembly = new Category { Name = "Montaż mebli" };
            var cleaning = new Category { Name = "Sprzątanie" };
            var humanCare = new Category { Name = "Opieka" };
            var renovation = new Category { Name = "Remont" };
            var mechanic = new Category { Name = "Mechanik" };
            var garden = new Category { Name = "Ogród" };
            var plumber = new Category { Name = "Hydraulik" };
            var transport = new Category { Name = "Transport" };
            var itSpec = new Category { Name = "Informatyk" };
            var other = new Category { Name = "Pozostałe" };
                
            IEnumerable<Category> categories =
            [
                electrician,
                handyman,
                furnitureAssembly,
                cleaning,
                humanCare,
                renovation,
                mechanic,
                garden,
                plumber,
                transport,
                itSpec,
                other
            ];

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
            
            var pending = new Status { Name = "Oczekujące" };
            var occupied = new Status { Name = "Zajęte" };
            var ended = new Status { Name = "Zakończone" };
            var planned = new Status { Name = "Zaplanowane" };
            var ongoing = new Status { Name = "Trwające" };
            var cancelled = new Status { Name = "Anulowane" };
            var completed = new Status { Name = "Ukończone" };
                
            IEnumerable<Status> statuses = 
            [
                pending,
                occupied,
                ended,
                planned,
                ongoing,
                cancelled,
                completed
            ];
            
            await dbContext.Statuses.AddRangeAsync(statuses);
            await dbContext.SaveChangesAsync();
            
            var task = new Type { Name = "Zlecenie" };
            var service = new Type { Name = "Usługa" };

            IEnumerable<Type> types =
            [
                task,
                service
            ];
            
            await dbContext.Types.AddRangeAsync(types);
            await dbContext.SaveChangesAsync();
        
        #endregion
        
        var user1 = new User {
            FullName = "Jan Kowalski",
            BirthDate = new DateOnly(1990, 8, 12),
            Email = "j.kowalski@wp.pl",
            PhoneNumber = "639371956"
        };
        user1.PasswordHash = userManager.PasswordHasher.HashPassword(user1, "Password1!");
        
        var user2 = new User {
            FullName = "Anna Nowak",
            BirthDate = new DateOnly(1985, 5, 23),
            Email = "anna.Utcnowak@o2.pl",
            PhoneNumber = "772916397"
        };
        user2.PasswordHash = userManager.PasswordHasher.HashPassword(user2, "Password1!");

        var user3 = new User {
            FullName = "Piotr Wiśniewski",
            BirthDate = new DateOnly(1978, 2, 15),
            Email = "piotrw78@wp.pl",
            PhoneNumber = "826993797"
        };
        user3.PasswordHash = userManager.PasswordHasher.HashPassword(user3, "Password1!");
        
        var user4 = new User {
            FullName = "Katarzyna Dąbrowska",
            BirthDate = new DateOnly(2000, 11, 29),
            Email = "dabek00@gmail.com",
            PhoneNumber = "648291739"
        };
        user4.PasswordHash = userManager.PasswordHasher.HashPassword(user4, "Password1!");

        var user5 = new User {
            FullName = "Marek Kowalczyk",
            BirthDate = new DateOnly(1995, 3, 7),
            Email = "marekkmail@onet.pl",
            PhoneNumber = "739121239"
        };
        user5.PasswordHash = userManager.PasswordHasher.HashPassword(user5, "Password1!");

        var user6 = new User {
            FullName = "Karolina Zając",
            BirthDate = new DateOnly(1992, 12, 3),
            Email = "karoza92@outlook.com",
            PhoneNumber = "826182881"
        };
        user6.PasswordHash = userManager.PasswordHasher.HashPassword(user6, "Password1!");

        var review1 = new Review {
            Reviewer = user3,
            Reviewee = user1,
            Rating = 5,
            Comment = "Bardzo dobrze radzi sobie z naprawą komputera. Polecam!",
            PostDateTime = DateTime.UtcNow.AddDays(-20)
        };
        user3.ReviewsGiven.Add(review1);
        user1.ReviewsReceived.Add(review1);

        var review2 = new Review {
            Reviewer = user3,
            Reviewee = user2,
            Rating = 1,
            Comment = "Nie polecam, nie dotrzymuje terminów.",
            PostDateTime = DateTime.UtcNow.AddDays(-25)
        };
        user3.ReviewsGiven.Add(review2);
        user2.ReviewsReceived.Add(review2);

        var review3 = new Review {
            Reviewer = user4,
            Reviewee = user1,
            Rating = 5,
            Comment = "Bardzo miła współpraca. Polecam!",
            PostDateTime = DateTime.UtcNow.AddDays(-24)
        };
        user4.ReviewsGiven.Add(review3);
        user1.ReviewsReceived.Add(review3);

        var review4 = new Review {
            Reviewer = user4,
            Reviewee = user2,
            Rating = 3,
            Comment = "Nieostrożnie podchodzi do wykończenia wnętrza. Widać niedociągnięcia.",
            PostDateTime = DateTime.UtcNow.AddDays(-15)
        };
        user4.ReviewsGiven.Add(review4);
        user2.ReviewsReceived.Add(review4);
        
        var offer1 = new Offer {
            Title = "Zlecę naprawę komputera",
            Description = "Potrzebuję pomocy w naprawie komputera. Komputer nie włącza się. Proszę o kontakt.",
            Price = 60.00m,
            PostDateTime = DateTime.UtcNow,
            ExpiryDateTime = DateTime.UtcNow.AddDays(7),
            Location = new Location
            {
                City = "Włocławek",
                Street = "Pogodna 5",
                ZipCode = "87-800",
                Latitude = 52.666492,
                Longitude = 19.038049
            },
            Type = task, // 1
            Status = ended, // 3
            Category = itSpec, // 11
            Provider = user3
        };
        user3.ProvidedOffers.Add(offer1);
        var contractorOffer101 = new ContractorOffer {
            Contractor = user6,
            Offer = offer1,
            Status = cancelled
        };
        var contractorOffer102 = new ContractorOffer {
            Contractor = user1,
            Offer = offer1,
            Status = completed
        };
        
        var offer2 = new Offer {
            Title = "Zlecę montaż mebli",
            Description = "Potrzebuję pomocy w montażu mebli. Proszę o kontakt.",
            Price = 150.00m,
            PostDateTime = DateTime.UtcNow,
            ExpiryDateTime = DateTime.UtcNow.AddDays(10),
            Location = new Location
            {
                City = "Włocławek",
                Street = "Świętego Antoniego 38",
                ZipCode = "87-800",
                Latitude = 52.652676,
                Longitude = 19.071314
            },
            Type = task, // 1
            Status = occupied, // 2
            Category = furnitureAssembly, // 3
            Provider = user3
        };
        user3.ProvidedOffers.Add(offer2);
        var contractorOffer201 = new ContractorOffer {
            Contractor = user2,
            Offer = offer2,
            Status = cancelled
        };
        var contractorOffer202 = new ContractorOffer {
            Contractor = user5,
            Offer = offer2,
            Status = planned
        };
        
        var offer3 = new Offer {
            Title = "Zlecę sprzątanie mieszkania",
            Description = "Potrzebuję pomocy w sprzątaniu mieszkania. Proszę o kontakt.",
            Price = 50.00m,
            PostDateTime = DateTime.UtcNow,
            ExpiryDateTime = DateTime.UtcNow.AddDays(5),
            Location = new Location
            {
                City = "Włocławek",
                Street = "Płocka 21",
                ZipCode = "87-800",
                Latitude = 52.654837,
                Longitude = 19.098801
            },
            Type = task, // 1
            Status = ended, // 3
            Category = cleaning, // 4
            Provider = user4
        };
        user4.ProvidedOffers.Add(offer3);
        var contractorOffer301 = new ContractorOffer {
            Contractor = user5,
            Offer = offer3,
            Status = cancelled
        };
        var contractorOffer302 = new ContractorOffer {
            Contractor = user6,
            Offer = offer3,
            Status = cancelled
        };
        var contractorOffer303 = new ContractorOffer {
            Contractor = user1,
            Offer = offer3,
            Status = completed
        };
        
        var offer4 = new Offer {
            Title = "Zlecę wykończenie wnętrza",
            Description = "Więcej informacji pod numerem telefonu.",
            Price = 500.00m,
            PostDateTime = DateTime.UtcNow,
            ExpiryDateTime = DateTime.UtcNow.AddDays(3),
            Location = new Location
            {
                City = "Włocławek",
                Street = "Grodzka 32",
                ZipCode = "87-800",
                Latitude = 52.673865,
                Longitude = 19.064747
            },
            Type = task, // 1
            Status = occupied, // 2
            Category = renovation, // 6
            Provider = user4
        };
        user4.ProvidedOffers.Add(offer4);
        var contractorOffer401 = new ContractorOffer {
            Contractor = user2,
            Offer = offer4,
            Status = planned
        };
        
        var offer5 = new Offer {
            Title = "Zlecę naprawę pralki",
            Description = "Potrzebuję pomocy w naprawie pralki. Pralka nie wiruje. Proszę o kontakt.",
            Price = 65.00m,
            PostDateTime = DateTime.UtcNow,
            ExpiryDateTime = DateTime.UtcNow.AddDays(9),
            Location = new Location
            {
                City = "Włocławek",
                Street = "Plac Kolanowszczyzna 16",
                ZipCode = "87-800",
                Latitude = 52.647224,
                Longitude = 19.076273
            },
            Type = task, // 1
            Status = pending, // 1
            Category = electrician, // 1
            Provider = user5
        };
        user5.ProvidedOffers.Add(offer5);
        var contractorOffer501 = new ContractorOffer {
            Contractor = user1,
            Offer = offer5,
            Status = cancelled
        };
        var contractorOffer502 = new ContractorOffer {
            Contractor = user2,
            Offer = offer5,
            Status = cancelled
        };
        var contractorOffer503 = new ContractorOffer {
            Contractor = user6,
            Offer = offer5,
            Status = cancelled
        };
        
        var offer6 = new Offer {
            Title = "Zlecę skoszenie trawnika",
            Description = "Działka o powierzchni 500m2. Więcej informacji na miejscu.",
            Price = 100.00m,
            PostDateTime = DateTime.UtcNow,
            ExpiryDateTime = DateTime.UtcNow.AddDays(7),
            Location = new Location
            {
                City = "Włocławek",
                Street = "Grodzka 123",
                ZipCode = "87-800",
                Latitude = 52.680049,
                Longitude = 19.047614
            },
            Type = task, // 1
            Status = pending, // 1
            Category = garden, // 8
            Provider = user1
        };
        user1.ProvidedOffers.Add(offer6);
        
        var offer7 = new Offer {
            Title = "Zlecę naprawę roweru",
            Description = "Potrzebuję pomocy w naprawie roweru. Proszę o kontakt.",
            Price = 20.00m,
            PostDateTime = DateTime.UtcNow,
            ExpiryDateTime = DateTime.UtcNow.AddDays(1),
            Location = new Location
            {
                City = "Włocławek",
                Street = "Stanisława Noakowskiego 43",
                ZipCode = "87-800",
                Latitude = 52.639425,
                Longitude = 19.042352
            },
            Type = task, // 1
            Status = pending, // 1
            Category = handyman, // 2
            Provider = user6
        };
        user6.ProvidedOffers.Add(offer7);
        var contractorOffer701 = new ContractorOffer {
            Contractor = user5,
            Offer = offer7,
            Status = cancelled
        };
        
        var offer8 = new Offer {
            Title = "Transport mebli",
            Description = "Przewozimy meble w całym mieście i do 50 km",
            Price = 750.00m,
            PostDateTime = DateTime.UtcNow,
            ExpiryDateTime = DateTime.UtcNow.AddDays(30),
            Location = new Location
            {
                City = "Włocławek",
                Street = "Chocimska 19",
                ZipCode = "87-800",
                Latitude = 52.66647,
                Longitude = 19.036092
            },
            Type = service, // 2
            Status = pending, // 1
            Category = transport, // 10
            Provider = user2
        };
        user2.ProvidedOffers.Add(offer8);
        var contractorOffer801 = new ContractorOffer {
            Contractor = user3,
            Offer = offer8,
            Status = cancelled
        };
        
        var offer9 = new Offer {
            Title = "Amatorski serwis informatyczny",
            Description = "Naprawa komputerów, instalacja oprogramowania, konfiguracja urządzeń sieciowych i wiele innych, zapraszam do kontaktu.",
            Price = 100.00m,
            PostDateTime = DateTime.UtcNow,
            ExpiryDateTime = DateTime.UtcNow.AddDays(30),
            Location = new Location
            {
                City = "Włocławek",
                Street = "Botaniczna 22",
                ZipCode = "87-810",
                Latitude = 52.606759,
                Longitude = 19.028311
            },
            Type = service, // 2
            Status = pending, // 1
            Category = itSpec, // 11
            Provider = user1
        };
        user1.ProvidedOffers.Add(offer9);
        
        IEnumerable<ContractorOffer> contractorOffers = 
        [
            contractorOffer101, contractorOffer102,
            contractorOffer201, contractorOffer202,
            contractorOffer301, contractorOffer302, contractorOffer303,
            contractorOffer401,
            contractorOffer501, contractorOffer502, contractorOffer503,
            contractorOffer701,
            contractorOffer801
        ];

        await dbContext.ContractorOffer.AddRangeAsync(contractorOffers);
        await dbContext.SaveChangesAsync();
    }
}