using DataAccess.Data;
using DataAccess.Models;

namespace IdentityApi
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                // Users 
                var users = new List<User>
                {
                    new User { FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@example.com", Password = "password1" },
                    new User { FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@example.com", Password = "password2" },
                    new User { FirstName = "Tomasz", LastName = "Wiśniewski", Email = "tomasz.wisniewski@example.com", Password = "password3" },
                    new User { FirstName = "Maria", LastName = "Kamińska", Email = "maria.kaminska@example.com", Password = "password4" },
                    new User { FirstName = "Paweł", LastName = "Zieliński", Email = "pawel.zielinski@example.com", Password = "password5" },
                    new User { FirstName = "Agnieszka", LastName = "Dąbrowska", Email = "agnieszka.dabrowska@example.com", Password = "password6" },
                    new User { FirstName = "Katarzyna", LastName = "Nowicka", Email = "katarzyna.nowicka@example.com", Password = "password7" },
                    new User { FirstName = "Michał", LastName = "Majewski", Email = "michal.majewski@example.com", Password = "password8" },
                    new User { FirstName = "Barbara", LastName = "Sikorska", Email = "barbara.sikorska@example.com", Password = "password9" },
                    new User { FirstName = "Piotr", LastName = "Jankowski", Email = "piotr.jankowski@example.com", Password = "password10" },
                    new User { FirstName = "Ewa", LastName = "Kaczmarek", Email = "ewa.kaczmarek@example.com", Password = "password11" },
                    new User { FirstName = "Grzegorz", LastName = "Sadowski", Email = "grzegorz.sadowski@example.com", Password = "password12" },
                    new User { FirstName = "Joanna", LastName = "Wróbel", Email = "joanna.wrobel@example.com", Password = "password13" },
                    new User { FirstName = "Andrzej", LastName = "Czerwiński", Email = "andrzej.czerwinski@example.com", Password = "password14" },
                    new User { FirstName = "Monika", LastName = "Ostrowska", Email = "monika.ostrowska@example.com", Password = "password15" },
                    new User { FirstName = "Rafał", LastName = "Zawadzki", Email = "rafal.zawadzki@example.com", Password = "password16" },
                    new User { FirstName = "Justyna", LastName = "Jastrzębska", Email = "justyna.jastrzebska@example.com", Password = "password17" },
                    new User { FirstName = "Karol", LastName = "Piotrowski", Email = "karol.piotrowski@example.com", Password = "password18" },
                    new User { FirstName = "Magdalena", LastName = "Wojciechowska", Email = "magdalena.wojciechowska@example.com", Password = "password19" },
                    new User { FirstName = "Łukasz", LastName = "Więckowski", Email = "lukasz.wieckowski@example.com", Password = "password20" },
                };

                context.Users.AddRange(users);
                context.SaveChanges();


                // Friends
                var random = new Random();

                foreach (var user in context.Users.ToList())
                {
                    int numberOfFriends = random.Next(1, 6);
                    var possibleFriends = users.Where(u => u.Id != user.Id).OrderBy(_ => random.Next()).Take(numberOfFriends).ToList();
                    user.Friends = new List<User>(possibleFriends);
                }
                context.SaveChanges();


                // Posts
                var posts = new List<Post>
                {
                    new Post { Content = "Hello world!", CreatedAt = DateTime.UtcNow.AddDays(-10), UserId = context.Users.First(u => u.Email == "jan.kowalski@example.com").Id },
                    new Post { Content = "First post here!", CreatedAt = DateTime.UtcNow.AddDays(-9), UserId = context.Users.First(u => u.Email == "anna.nowak@example.com").Id },
                    new Post { Content = "Loving this app!", CreatedAt = DateTime.UtcNow.AddDays(-8), UserId = context.Users.First(u => u.Email == "tomasz.wisniewski@example.com").Id },
                    new Post { Content = "Such a beautiful day!", CreatedAt = DateTime.UtcNow.AddDays(-7), UserId = context.Users.First(u => u.Email == "maria.kaminska@example.com").Id },
                    new Post { Content = "Anyone up for coffee?", CreatedAt = DateTime.UtcNow.AddDays(-6), UserId = context.Users.First(u => u.Email == "jan.kowalski@example.com").Id },
                    new Post { Content = "Learning new things every day.", CreatedAt = DateTime.UtcNow.AddDays(-5), UserId = context.Users.First(u => u.Email == "anna.nowak@example.com").Id },
                    new Post { Content = "This app is awesome!", CreatedAt = DateTime.UtcNow.AddDays(-4), UserId = context.Users.First(u => u.Email == "tomasz.wisniewski@example.com").Id },
                    new Post { Content = "Weekend vibes!", CreatedAt = DateTime.UtcNow.AddDays(-3), UserId = context.Users.First(u => u.Email == "maria.kaminska@example.com").Id },
                    new Post { Content = "Just finished a great book!", CreatedAt = DateTime.UtcNow.AddDays(-2), UserId = context.Users.First(u => u.Email == "jan.kowalski@example.com").Id },
                    new Post { Content = "Excited for tomorrow's event!", CreatedAt = DateTime.UtcNow.AddDays(-1), UserId = context.Users.First(u => u.Email == "anna.nowak@example.com").Id }
                };

                context.Posts.AddRange(posts);
                context.SaveChanges();


                // Likes
                var allUsers = context.Users.ToList();
                var allPosts = context.Posts.ToList();

                var likes = new List<PostLike>();

                foreach (var post in allPosts)
                {
                    int numberOfLikes = random.Next(1, 11);

                    var usersWhoLike = allUsers.OrderBy(_ => random.Next()).Take(numberOfLikes).ToList();

                    foreach (var user in usersWhoLike)
                    {
                        if (!likes.Any(l => l.UserId == user.Id && l.PostId == post.Id))
                        {
                            likes.Add(new PostLike
                            {
                                UserId = user.Id,
                                PostId = post.Id,
                                LikedAt = DateTime.UtcNow.AddMinutes(-random.Next(1, 1000)) 
                            });
                        }
                    }
                }

                context.PostLikes.AddRange(likes);
                context.SaveChanges();

            }
        }
           
    }
}
