using Microsoft.AspNetCore.Identity;
using ZlecajGo.Domain.Constants;
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
        
            var electrician = new Category { Name = AppCategories.Electrician.Name };
            var handyman = new Category { Name = AppCategories.Handyman.Name };
            var furnitureAssembly = new Category { Name = AppCategories.FurnitureAssembly.Name };
            var cleaning = new Category { Name = AppCategories.Cleaning.Name };
            var humanCare = new Category { Name = AppCategories.HumanCare.Name };
            var renovation = new Category { Name = AppCategories.Renovation.Name };
            var mechanic = new Category { Name = AppCategories.Mechanic.Name };
            var garden = new Category { Name = AppCategories.Garden.Name };
            var plumber = new Category { Name = AppCategories.Plumber.Name };
            var transport = new Category { Name = AppCategories.Transport.Name };
            var itSpec = new Category { Name = AppCategories.ItSpecialist.Name };
            var other = new Category { Name = AppCategories.Other.Name };
                
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
            
            var pending = new Status { Name = AppStatuses.Pending.Name };
            var occupied = new Status { Name = AppStatuses.Occupied.Name };
            var ended = new Status { Name = AppStatuses.Ended.Name };
            var planned = new Status { Name = AppStatuses.Planned.Name };
            var ongoing = new Status { Name = AppStatuses.Ongoing.Name };
            var cancelled = new Status { Name = AppStatuses.Cancelled.Name };
            var completed = new Status { Name = AppStatuses.Completed.Name };
                
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
            
            var task = new Type { Name = AppTypes.Task.Name };
            var service = new Type { Name = AppTypes.Service.Name };

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
            UserName = "janek90",
            NormalizedUserName = "JANEK90",
            Email = "j.kowalski@wp.pl",
            NormalizedEmail = "J.KOWALSKI@WP.PL",
            EmailConfirmed = true,
            PhoneNumber = "+48639371956",
            PhoneNumberConfirmed = true,
            IsProfileCompleted = true
        };
        user1.PasswordHash = userManager.PasswordHasher.HashPassword(user1, "Password1!");
        
        var user2 = new User {
            FullName = "Anna Nowak",
            BirthDate = new DateOnly(1985, 5, 23),
            UserName = "nowakowska85",
            NormalizedUserName = "NOWAKOWSKA85",
            Email = "anna.nowak@o2.pl",
            NormalizedEmail = "ANNA.NOWAK@O2.PL",
            EmailConfirmed = true,
            PhoneNumber = "+48772916397",
            PhoneNumberConfirmed = true,
            IsProfileCompleted = true
        };
        user2.PasswordHash = userManager.PasswordHasher.HashPassword(user2, "Password1!");

        var user3 = new User {
            FullName = "Piotr Wiśniewski",
            BirthDate = new DateOnly(1978, 2, 15),
            UserName = "wiśnia78",
            NormalizedUserName = "WIŚNIA78",
            Email = "piotrw78@wp.pl",
            NormalizedEmail = "PIOTRW78@WP.PL",
            EmailConfirmed = true,
            PhoneNumber = "+48826993797",
            PhoneNumberConfirmed = true,
            IsProfileCompleted = true
        };
        user3.PasswordHash = userManager.PasswordHasher.HashPassword(user3, "Password1!");
        
        var user4 = new User {
            FullName = "Katarzyna Dąbrowska",
            BirthDate = new DateOnly(2000, 11, 29),
            UserName = "kasiadab",
            NormalizedUserName = "KASIADAB",
            Email = "dabek00@gmail.com",
            NormalizedEmail = "DABEK00@GMAIL.COM",
            EmailConfirmed = true,
            PhoneNumber = "+48648291739",
            PhoneNumberConfirmed = true,
            IsProfileCompleted = true
        };
        user4.PasswordHash = userManager.PasswordHasher.HashPassword(user4, "Password1!");

        var user5 = new User {
            FullName = "Marek Kowalczyk",
            BirthDate = new DateOnly(1995, 3, 7),
            UserName = "mariok",
            NormalizedUserName = "MARIOK",
            Email = "marekkmail@onet.pl",
            NormalizedEmail = "MAREKMAIL@ONET.PL",
            EmailConfirmed = true,
            PhoneNumber = "+48739121239",
            PhoneNumberConfirmed = true,
            IsProfileCompleted = true
        };
        user5.PasswordHash = userManager.PasswordHasher.HashPassword(user5, "Password1!");

        var user6 = new User {
            FullName = "Karolina Zając",
            BirthDate = new DateOnly(1992, 12, 3),
            UserName = "fenia",
            NormalizedUserName = "FENIA",
            Email = "karoza92@outlook.com",
            NormalizedEmail = "KAROZA92@OUTLOOK.COM",
            EmailConfirmed = true,
            PhoneNumber = "+48826182881",
            PhoneNumberConfirmed = true,
            IsProfileCompleted = true
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
        var offerContractor101 = new OfferContractor {
            Contractor = user6,
            Offer = offer1,
            Status = cancelled
        };
        var offerContractor102 = new OfferContractor {
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
        var offerContractor201 = new OfferContractor {
            Contractor = user2,
            Offer = offer2,
            Status = cancelled
        };
        var offerContractor202 = new OfferContractor {
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
        var offerContractor301 = new OfferContractor {
            Contractor = user5,
            Offer = offer3,
            Status = cancelled
        };
        var offerContractor302 = new OfferContractor {
            Contractor = user6,
            Offer = offer3,
            Status = cancelled
        };
        var offerContractor303 = new OfferContractor {
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
        var offerContractor401 = new OfferContractor {
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
        var offerContractor501 = new OfferContractor {
            Contractor = user1,
            Offer = offer5,
            Status = cancelled
        };
        var offerContractor502 = new OfferContractor {
            Contractor = user2,
            Offer = offer5,
            Status = cancelled
        };
        var offerContractor503 = new OfferContractor {
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
        var offerContractor701 = new OfferContractor {
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
        var offerContractor801 = new OfferContractor {
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
        
        IEnumerable<OfferContractor> offerContractors = 
        [
            offerContractor101, offerContractor102,
            offerContractor201, offerContractor202,
            offerContractor301, offerContractor302, offerContractor303,
            offerContractor401,
            offerContractor501, offerContractor502, offerContractor503,
            offerContractor701,
            offerContractor801
        ];

        await dbContext.OfferContractors.AddRangeAsync(offerContractors);
        await dbContext.SaveChangesAsync();
    }
}