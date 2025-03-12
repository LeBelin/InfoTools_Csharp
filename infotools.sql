-- --------------------------------------------------------
-- Hôte:                         127.0.0.1
-- Version du serveur:           8.0.30 - MySQL Community Server - GPL
-- SE du serveur:                Win64
-- HeidiSQL Version:             12.1.0.6537
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Listage de la structure de la base pour infotools
CREATE DATABASE IF NOT EXISTS `infotools` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `infotools`;

-- Listage de la structure de table infotools. clients
CREATE TABLE IF NOT EXISTS `clients` (
  `idClient` int NOT NULL AUTO_INCREMENT,
  `nomClient` char(50) DEFAULT NULL,
  `prenomClient` char(50) DEFAULT NULL,
  `emailClient` char(50) DEFAULT NULL,
  `telephoneClient` decimal(10,0) DEFAULT NULL,
  `adresseClient` char(50) DEFAULT NULL,
  `dateCreationClient` char(50) DEFAULT NULL,
  `idCommerciaux` int NOT NULL,
  PRIMARY KEY (`idClient`),
  KEY `idCommerciaux` (`idCommerciaux`),
  CONSTRAINT `clients_ibfk_1` FOREIGN KEY (`idCommerciaux`) REFERENCES `commerciaux` (`idCommerciaux`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.clients : ~1 rows (environ)
INSERT INTO `clients` (`idClient`, `nomClient`, `prenomClient`, `emailClient`, `telephoneClient`, `adresseClient`, `dateCreationClient`, `idCommerciaux`) VALUES
	(6, 'Deluxe', 'Dio', 'ab@gamil.com', 4554544, '18 avenue', '11/12/2024', 8);

-- Listage de la structure de table infotools. commerciaux
CREATE TABLE IF NOT EXISTS `commerciaux` (
  `idCommerciaux` int NOT NULL AUTO_INCREMENT,
  `nomCommerciaux` varchar(50) DEFAULT NULL,
  `prenomCommerciaux` varchar(50) DEFAULT NULL,
  `adresseCommerciaux` varchar(50) DEFAULT NULL,
  `villeCommerciaux` varchar(50) DEFAULT NULL,
  `cpCommerciaux` decimal(5,0) DEFAULT NULL,
  `mailCommerciaux` varchar(50) DEFAULT NULL,
  `telCommerciaux` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`idCommerciaux`),
  UNIQUE KEY `mailCommerciaux` (`mailCommerciaux`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.commerciaux : ~1 rows (environ)
INSERT INTO `commerciaux` (`idCommerciaux`, `nomCommerciaux`, `prenomCommerciaux`, `adresseCommerciaux`, `villeCommerciaux`, `cpCommerciaux`, `mailCommerciaux`, `telCommerciaux`) VALUES
	(8, 'Belin', 'Andy', '18 Avenue', 'C/S', 71100, 'ab@gmail.com', 677556699);

-- Listage de la structure de table infotools. factures
CREATE TABLE IF NOT EXISTS `factures` (
  `idFacture` int NOT NULL AUTO_INCREMENT,
  `dateFacture` char(50) DEFAULT NULL,
  `montantTotal` int DEFAULT NULL,
  `statutFacture` int DEFAULT NULL,
  `datePaiment` char(50) DEFAULT NULL,
  `numProduit` int DEFAULT NULL,
  `idClient` int NOT NULL,
  PRIMARY KEY (`idFacture`),
  KEY `idClient` (`idClient`),
  KEY `produit_FK2` (`numProduit`),
  CONSTRAINT `factures_ibfk_1` FOREIGN KEY (`idClient`) REFERENCES `clients` (`idClient`),
  CONSTRAINT `produit_FK2` FOREIGN KEY (`numProduit`) REFERENCES `produit` (`idProduit`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.factures : ~1 rows (environ)
INSERT INTO `factures` (`idFacture`, `dateFacture`, `montantTotal`, `statutFacture`, `datePaiment`, `numProduit`, `idClient`) VALUES
	(4, '06/12/2024', 4654, 1, '21/12/2024', 2, 6);

-- Listage de la structure de table infotools. failed_jobs
CREATE TABLE IF NOT EXISTS `failed_jobs` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `uuid` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `connection` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `queue` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `payload` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `exception` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `failed_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `failed_jobs_uuid_unique` (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools.failed_jobs : ~0 rows (environ)

-- Listage de la structure de table infotools. migrations
CREATE TABLE IF NOT EXISTS `migrations` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `migration` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `batch` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools.migrations : ~11 rows (environ)
INSERT INTO `migrations` (`id`, `migration`, `batch`) VALUES
	(1, '2014_10_12_000000_create_users_table', 1),
	(2, '2014_10_12_100000_create_password_reset_tokens_table', 1),
	(3, '2014_10_12_200000_add_two_factor_columns_to_users_table', 1),
	(4, '2019_08_19_000000_create_failed_jobs_table', 1),
	(5, '2019_12_14_000001_create_personal_access_tokens_table', 1),
	(6, '2020_05_21_100000_create_teams_table', 1),
	(7, '2020_05_21_200000_create_team_user_table', 1),
	(8, '2020_05_21_300000_create_team_invitations_table', 1),
	(9, '2024_10_02_131407_create_sessions_table', 1),
	(10, '2024_10_02_144630_create_commercials_table', 2),
	(11, '2024_10_09_113830_create_commerciauxes_table', 3);

-- Listage de la structure de table infotools. password_reset_tokens
CREATE TABLE IF NOT EXISTS `password_reset_tokens` (
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `token` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools.password_reset_tokens : ~0 rows (environ)

-- Listage de la structure de table infotools. personal_access_tokens
CREATE TABLE IF NOT EXISTS `personal_access_tokens` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `tokenable_type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `tokenable_id` bigint unsigned NOT NULL,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `token` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `abilities` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `last_used_at` timestamp NULL DEFAULT NULL,
  `expires_at` timestamp NULL DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `personal_access_tokens_token_unique` (`token`),
  KEY `personal_access_tokens_tokenable_type_tokenable_id_index` (`tokenable_type`,`tokenable_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools.personal_access_tokens : ~0 rows (environ)
INSERT INTO `personal_access_tokens` (`id`, `tokenable_type`, `tokenable_id`, `name`, `token`, `abilities`, `last_used_at`, `expires_at`, `created_at`, `updated_at`) VALUES
	(1, 'App\\Models\\User', 3, 'test read', '79948b00a16d9cfac00a4c8ff87318121ab634c8a86ed28a2112a077e61ec281', '["read","view-rendezvous"]', '2025-01-08 12:55:05', NULL, '2025-01-08 12:54:45', '2025-01-08 13:12:45');

-- Listage de la structure de table infotools. produit
CREATE TABLE IF NOT EXISTS `produit` (
  `idProduit` int NOT NULL AUTO_INCREMENT,
  `nomProduit` char(50) DEFAULT NULL,
  `desciptionProduit` text,
  `prixUnitaire` int DEFAULT NULL,
  `dateAjoutProduit` char(50) DEFAULT NULL,
  `imgProduit` char(50) DEFAULT NULL,
  PRIMARY KEY (`idProduit`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.produit : ~1 rows (environ)
INSERT INTO `produit` (`idProduit`, `nomProduit`, `desciptionProduit`, `prixUnitaire`, `dateAjoutProduit`, `imgProduit`) VALUES
	(2, 'Hebergeur v1', 'bla bla bla', 500, '06/12/2024', 'img/Omi.png'),
	(4, 'Hebergeur v2', 'bla bla bla', 500, '20/12/2024', 'img/Omi.png');

-- Listage de la structure de table infotools. prospects
CREATE TABLE IF NOT EXISTS `prospects` (
  `idProspect` int NOT NULL AUTO_INCREMENT,
  `nomProspect` char(50) DEFAULT NULL,
  `prenomProspect` char(50) DEFAULT NULL,
  `telephoneProspect` decimal(10,0) DEFAULT NULL,
  `emailProspect` char(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `dateCreation` char(50) DEFAULT NULL,
  `idCommerciaux` int NOT NULL,
  PRIMARY KEY (`idProspect`),
  KEY `idCommerciaux` (`idCommerciaux`),
  CONSTRAINT `prospects_ibfk_1` FOREIGN KEY (`idCommerciaux`) REFERENCES `commerciaux` (`idCommerciaux`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.prospects : ~2 rows (environ)
INSERT INTO `prospects` (`idProspect`, `nomProspect`, `prenomProspect`, `telephoneProspect`, `emailProspect`, `dateCreation`, `idCommerciaux`) VALUES
	(6, 'Deluxe', 'Dio', 527485, 'ab@gamil.com', '06/12/2024', 8);

-- Listage de la structure de table infotools. rendezvous
CREATE TABLE IF NOT EXISTS `rendezvous` (
  `idRendezVous` int NOT NULL AUTO_INCREMENT,
  `dateRendezVous` char(50) DEFAULT NULL,
  `descriptionRendezVous` text,
  `heureDebutRendezVous` time DEFAULT NULL,
  `heureFinRendezVous` time DEFAULT NULL,
  `idCommerciaux` int NOT NULL,
  `idProspect` int NOT NULL,
  `idClient` int NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idRendezVous`),
  KEY `idCommerciaux` (`idCommerciaux`),
  KEY `idProspect` (`idProspect`),
  KEY `idClient` (`idClient`),
  CONSTRAINT `rendezvous_ibfk_1` FOREIGN KEY (`idCommerciaux`) REFERENCES `commerciaux` (`idCommerciaux`),
  CONSTRAINT `rendezvous_ibfk_2` FOREIGN KEY (`idProspect`) REFERENCES `prospects` (`idProspect`),
  CONSTRAINT `rendezvous_ibfk_3` FOREIGN KEY (`idClient`) REFERENCES `clients` (`idClient`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.rendezvous : ~1 rows (environ)
INSERT INTO `rendezvous` (`idRendezVous`, `dateRendezVous`, `descriptionRendezVous`, `heureDebutRendezVous`, `heureFinRendezVous`, `idCommerciaux`, `idProspect`, `idClient`, `created_at`, `updated_at`) VALUES
	(10, '05/12/2024', 'bla bla bla', '09:00:00', '12:00:00', 8, 6, 6, '2025-01-08 12:45:27', '2025-01-08 12:45:27');

-- Listage de la structure de table infotools. sessions
CREATE TABLE IF NOT EXISTS `sessions` (
  `id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `user_id` bigint unsigned DEFAULT NULL,
  `ip_address` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `user_agent` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `payload` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `last_activity` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `sessions_user_id_index` (`user_id`),
  KEY `sessions_last_activity_index` (`last_activity`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools.sessions : ~5 rows (environ)
INSERT INTO `sessions` (`id`, `user_id`, `ip_address`, `user_agent`, `payload`, `last_activity`) VALUES
	('3jxAQLquo51V70ax76OQ3UXa5eZU8Ozgn2GVTtrk', 5, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36', 'YTo1OntzOjY6Il90b2tlbiI7czo0MDoiWFZCbWNmcWFyYXBISzlubDdBdlZ2cWV6ajJYY2JTRzBQdmJoYW1JcyI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6Mjk6Imh0dHA6Ly9pbmZvdG9vbHMudGVzdC9jb3VudGVyIjt9czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319czo1MDoibG9naW5fd2ViXzU5YmEzNmFkZGMyYjJmOTQwMTU4MGYwMTRjN2Y1OGVhNGUzMDk4OWQiO2k6NTtzOjIxOiJwYXNzd29yZF9oYXNoX3NhbmN0dW0iO3M6NjA6IiQyeSQxMiRaVjR4QWoud0JUWkduODh6OGczNEJ1SXhocTlLdi5Sc1YxdGlZU2tOdWFxbWRnN3FscUM3dSI7fQ==', 1741786216),
	('hHkfgRaI6Dybg0Ny9B5IxqsOL4TGyOTVZzYdhFm0', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoiSjdPUGVCVm1Sa3NITmhNTXFYTVBxeVJYVHNvN0dTa0lBRTRORDdIWiI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6MzA6Imh0dHA6Ly9pbmZvdG9vbHMudGVzdC9yZWdpc3RlciI7fXM6NjoiX2ZsYXNoIjthOjI6e3M6Mzoib2xkIjthOjA6e31zOjM6Im5ldyI7YTowOnt9fX0=', 1741785735);

-- Listage de la structure de table infotools. teams
CREATE TABLE IF NOT EXISTS `teams` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `user_id` bigint unsigned NOT NULL,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `personal_team` tinyint(1) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `teams_user_id_index` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools.teams : ~4 rows (environ)
INSERT INTO `teams` (`id`, `user_id`, `name`, `personal_team`, `created_at`, `updated_at`) VALUES
	(1, 1, 'Sofiane\'s Team', 1, '2024-10-09 08:54:04', '2024-10-09 08:54:04'),
	(2, 2, 'Mouloud\'s Team', 1, '2024-10-09 10:15:13', '2024-10-09 10:15:13'),
	(3, 3, 'Andy\'s Team', 1, '2025-01-08 11:34:07', '2025-01-08 11:34:07'),
	(4, 3, 'Test', 0, '2025-01-08 13:13:31', '2025-01-08 13:13:31'),
	(5, 4, 'Test\'s Team', 1, '2025-01-08 13:16:00', '2025-01-08 13:16:00'),
	(6, 5, 'Andy\'s Team', 1, '2025-03-12 12:26:27', '2025-03-12 12:26:27');

-- Listage de la structure de table infotools. team_invitations
CREATE TABLE IF NOT EXISTS `team_invitations` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `team_id` bigint unsigned NOT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `role` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_invitations_team_id_email_unique` (`team_id`,`email`),
  CONSTRAINT `team_invitations_team_id_foreign` FOREIGN KEY (`team_id`) REFERENCES `teams` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools.team_invitations : ~0 rows (environ)
INSERT INTO `team_invitations` (`id`, `team_id`, `email`, `role`, `created_at`, `updated_at`) VALUES
	(1, 4, 'test@gmail.com', 'view-rendezvous', '2025-01-08 13:16:15', '2025-01-08 13:16:15');

-- Listage de la structure de table infotools. team_user
CREATE TABLE IF NOT EXISTS `team_user` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `team_id` bigint unsigned NOT NULL,
  `user_id` bigint unsigned NOT NULL,
  `role` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `team_user_team_id_user_id_unique` (`team_id`,`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools.team_user : ~0 rows (environ)

-- Listage de la structure de table infotools. users
CREATE TABLE IF NOT EXISTS `users` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `email_verified_at` timestamp NULL DEFAULT NULL,
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `two_factor_secret` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `two_factor_recovery_codes` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `two_factor_confirmed_at` timestamp NULL DEFAULT NULL,
  `remember_token` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `current_team_id` bigint unsigned DEFAULT NULL,
  `profile_photo_path` varchar(2048) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `users_email_unique` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools.users : ~3 rows (environ)
INSERT INTO `users` (`id`, `name`, `email`, `email_verified_at`, `password`, `two_factor_secret`, `two_factor_recovery_codes`, `two_factor_confirmed_at`, `remember_token`, `current_team_id`, `profile_photo_path`, `created_at`, `updated_at`) VALUES
	(1, 'Sofiane Matjaouj-Devaivre', 'sosomatj@gmail.com', NULL, 'jsp', NULL, NULL, NULL, NULL, 1, NULL, '2024-10-09 08:54:04', '2024-10-09 08:54:05'),
	(2, 'Mouloud', 'farouk@jmail.com', NULL, '$2y$12$5i1BpUbAQRHIhdTOt.l7X.jYvvFOsNqJh1qmeKmqMUz7.356wPKWi', NULL, NULL, NULL, NULL, 2, NULL, '2024-10-09 10:15:13', '2024-10-09 10:15:13'),
	(4, 'Test', 'test@gmail.com', NULL, '$2y$12$qyPEnweHp8gbUzvjYbjeFugfAN65AFD0WRbMJSQyQ7nZ7NIkIhdve', NULL, NULL, NULL, NULL, 5, NULL, '2025-01-08 13:16:00', '2025-01-08 13:16:00'),
	(5, 'Andy', 'andyb@gmail.com', NULL, '$2y$12$ZV4xAj.wBTZGn88z8g34BuIxhq9Kv.RsV1tiYSkNuaqmdg7qlqC7u', NULL, NULL, NULL, NULL, 6, NULL, '2025-03-12 12:26:27', '2025-03-12 12:26:28');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
