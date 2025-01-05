CREATE DATABASE PostItAppDb;
GO
USE PostItAppDb;
GO

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

CREATE TABLE Posts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Content NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE PostLikes (
    UserId INT NOT NULL,
    PostId INT NOT NULL,
    LikedAt DATETIME NOT NULL,
    PRIMARY KEY (UserId, PostId),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE NO ACTION,
    FOREIGN KEY (PostId) REFERENCES Posts(Id) ON DELETE NO ACTION
);

CREATE TABLE UserFriend (
    FriendId INT NOT NULL,
    UserId INT NOT NULL,
    PRIMARY KEY (FriendId, UserId),
    FOREIGN KEY (FriendId) REFERENCES Users(Id) ON DELETE NO ACTION,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE NO ACTION
);

INSERT INTO Users (FirstName, LastName, Email, Password) VALUES
('Jan', 'Kowalski', 'jan.kowalski@example.com', 'password1'),
('Anna', 'Nowak', 'anna.nowak@example.com', 'password2'),
('Tomasz', 'Wiśniewski', 'tomasz.wisniewski@example.com', 'password3'),
('Maria', 'Kamińska', 'maria.kaminska@example.com', 'password4'),
('Paweł', 'Zieliński', 'pawel.zielinski@example.com', 'password5'),
('Agnieszka', 'Dąbrowska', 'agnieszka.dabrowska@example.com', 'password6'),
('Katarzyna', 'Nowicka', 'katarzyna.nowicka@example.com', 'password7'),
('Michał', 'Majewski', 'michal.majewski@example.com', 'password8'),
('Barbara', 'Sikorska', 'barbara.sikorska@example.com', 'password9'),
('Piotr', 'Jankowski', 'piotr.jankowski@example.com', 'password10'),
('Ewa', 'Kaczmarek', 'ewa.kaczmarek@example.com', 'password11'),
('Grzegorz', 'Sadowski', 'grzegorz.sadowski@example.com', 'password12'),
('Joanna', 'Wróbel', 'joanna.wrobel@example.com', 'password13'),
('Andrzej', 'Czerwiński', 'andrzej.czerwinski@example.com', 'password14'),
('Monika', 'Ostrowska', 'monika.ostrowska@example.com', 'password15'),
('Rafał', 'Zawadzki', 'rafal.zawadzki@example.com', 'password16'),
('Justyna', 'Jastrzębska', 'justyna.jastrzebska@example.com', 'password17'),
('Karol', 'Piotrowski', 'karol.piotrowski@example.com', 'password18'),
('Magdalena', 'Wojas', 'magdalena.wojas@example.com', 'password19'),
('Łukasz', 'Więckowski', 'lukasz.wieckowski@example.com', 'password20');

INSERT INTO UserFriend (FriendId, UserId) VALUES
(9, 1), (19, 1),
(1, 2), (7, 2), (9, 2), (14, 2),
(2, 3), (4, 3), (7, 3), (13, 3),
(19, 4),
(10, 5),
(1, 6), (18, 6),
(5, 7), (9, 7), (13, 7), (16, 7), (19, 7),
(3, 8), (6, 8), (14, 8),
(5, 9), (8, 9), (11, 9), (14, 9), (20, 9),
(6, 10), (13, 10),
(12, 11), (18, 11), (20, 11),
(8, 12), (11, 12), (17, 12), (19, 12), (20, 12),
(9, 13),
(2, 14), (9, 14), (13, 14), (18, 14),
(12, 15),
(14, 16),
(2, 17), (3, 17), (10, 17), (19, 17), (20, 17),
(3, 18), (7, 18), (14, 18), (20, 18),
(15, 19), (17, 19),
(6, 20);

INSERT INTO Posts (Content, CreatedAt, UserId) VALUES
('Hello world!', '2024-12-16 12:29:51', 1),
('First post here!', '2024-12-17 12:29:51', 2),
('Loving this app!', '2024-12-18 12:29:51', 3),
('Such a beautiful day!', '2024-12-19 12:29:51', 4),
('Anyone up for coffee?', '2024-12-20 12:29:51', 1),
('Learning new things every day.', '2024-12-21 12:29:51', 2),
('This app is awesome!', '2024-12-22 12:29:51', 3),
('Weekend vibes!', '2024-12-23 12:29:51', 4),
('Just finished a great book!', '2024-12-24 12:29:51', 1),
('Excited for tomorrow''s event!', '2024-12-25 12:29:51', 2);

INSERT INTO PostLikes (UserId, PostId, LikedAt) VALUES
(1, 1, '2024-12-26 03:21:51'),
(1, 5, '2024-12-26 12:13:51'),
(1, 6, '2024-12-26 06:45:51'),
(2, 10, '2024-12-25 21:15:51'),
(3, 5, '2024-12-26 08:56:51'),
(3, 7, '2024-12-25 23:31:51'),
(4, 2, '2024-12-25 20:05:51'),
(4, 9, '2024-12-26 03:56:51'),
(6, 4, '2024-12-26 12:20:51'),
(6, 6, '2024-12-26 08:40:51'),
(6, 7, '2024-12-26 00:49:51'),
(6, 9, '2024-12-26 08:29:51'),
(7, 6, '2024-12-25 20:11:51'),
(7, 7, '2024-12-26 04:44:51'),
(8, 5, '2024-12-25 22:23:51'),
(8, 7, '2024-12-26 03:15:51'),
(8, 8, '2024-12-26 01:30:51'),
(10, 1, '2024-12-26 08:14:51'),
(10, 2, '2024-12-26 08:56:51'),
(10, 3, '2024-12-25 23:48:51'),
(10, 4, '2024-12-26 09:59:51'),
(10, 6, '2024-12-26 07:54:51'),
(10, 9, '2024-12-26 00:23:51'),
(11, 1, '2024-12-26 09:47:51'),
(11, 6, '2024-12-26 10:03:51'),
(11, 7, '2024-12-26 05:31:51'),
(11, 10, '2024-12-25 22:01:51'),
(12, 7, '2024-12-26 08:03:51'),
(14, 1, '2024-12-25 23:54:51'),
(14, 5, '2024-12-26 12:10:51'),
(14, 10, '2024-12-25 21:43:51'),
(15, 1, '2024-12-25 23:10:51'),
(15, 4, '2024-12-26 08:37:51'),
(15, 6, '2024-12-26 09:08:51'),
(15, 9, '2024-12-26 00:55:51'),
(15, 10, '2024-12-25 21:12:51'),
(16, 1, '2024-12-26 11:33:51'),
(16, 6, '2024-12-25 23:31:51'),
(17, 1, '2024-12-26 00:34:51'),
(17, 5, '2024-12-26 09:24:51'),
(17, 6, '2024-12-25 23:16:51'),
(18, 1, '2024-12-26 05:41:51'),
(18, 4, '2024-12-26 12:08:51'),
(18, 7, '2024-12-26 10:09:51'),
(18, 9, '2024-12-26 07:46:51'),
(18, 10, '2024-12-26 01:42:51'),
(19, 1, '2024-12-26 06:50:51'),
(20, 1, '2024-12-26 09:35:51'),
(20, 7, '2024-12-26 11:20:51'),
(20, 8, '2024-12-25 23:06:51'),
(20, 10, '2024-12-26 09:03:51');
