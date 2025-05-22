-- --------------------------------------------------------
-- Hôte:                         127.0.0.1
-- Version du serveur:           8.4.2 - MySQL Community Server - GPL
-- SE du serveur:                Win64
-- HeidiSQL Version:             12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Listage de la structure de la base pour infotools-new
CREATE DATABASE IF NOT EXISTS `infotools-new` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `infotools-new`;

-- Listage de la structure de table infotools-new. cache
CREATE TABLE IF NOT EXISTS `cache` (
  `key` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `value` mediumtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `expiration` int NOT NULL,
  PRIMARY KEY (`key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.cache : ~0 rows (environ)
DELETE FROM `cache`;

-- Listage de la structure de table infotools-new. cache_locks
CREATE TABLE IF NOT EXISTS `cache_locks` (
  `key` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `owner` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `expiration` int NOT NULL,
  PRIMARY KEY (`key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.cache_locks : ~0 rows (environ)
DELETE FROM `cache_locks`;

-- Listage de la structure de table infotools-new. clients
CREATE TABLE IF NOT EXISTS `clients` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `nom` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `telephone` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `adresse` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.clients : ~2 rows (environ)
DELETE FROM `clients`;
INSERT INTO `clients` (`id`, `nom`, `email`, `telephone`, `adresse`, `created_at`, `updated_at`) VALUES
	(2, 'Andy Belin', 'andy@gmail.com', '02 92 48 84 84', '18 avenue pierre nugues, Chalon Sur Saone', '2025-03-21 14:45:30', '2025-04-16 12:29:39'),
	(6, 'Sofiane', 'sofiane@gmail.com', '09 32 33 33 23', '18 route de la route sans nom', '2025-04-16 12:32:56', '2025-04-16 12:32:56');

-- Listage de la structure de table infotools-new. commandes
CREATE TABLE IF NOT EXISTS `commandes` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `client_id` bigint unsigned NOT NULL,
  `montant_total` decimal(10,2) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `client_id` (`client_id`),
  CONSTRAINT `commandes_ibfk_1` FOREIGN KEY (`client_id`) REFERENCES `clients` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.commandes : ~5 rows (environ)
DELETE FROM `commandes`;
INSERT INTO `commandes` (`id`, `client_id`, `montant_total`, `created_at`, `updated_at`) VALUES
	(1, 2, 396.00, '2025-04-10 10:46:38', '2025-04-14 13:29:14'),
	(2, 2, 512.00, '2025-04-10 10:48:31', '2025-04-14 12:16:22'),
	(5, 2, 76.32, '2025-04-10 10:55:53', '2025-04-10 10:55:53'),
	(7, 2, 164.00, '2025-04-10 11:00:22', '2025-04-10 11:00:22'),
	(13, 2, 379.76, '2025-04-16 10:58:23', '2025-04-16 10:58:23');

-- Listage de la structure de table infotools-new. commande_produit
CREATE TABLE IF NOT EXISTS `commande_produit` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `commande_id` bigint unsigned NOT NULL,
  `produit_id` bigint unsigned NOT NULL,
  `quantite` int NOT NULL,
  `prix_unitaire` decimal(10,2) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `commande_id` (`commande_id`),
  KEY `produit_id` (`produit_id`),
  CONSTRAINT `commande_produit_ibfk_1` FOREIGN KEY (`commande_id`) REFERENCES `commandes` (`id`) ON DELETE CASCADE,
  CONSTRAINT `commande_produit_ibfk_2` FOREIGN KEY (`produit_id`) REFERENCES `produits` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.commande_produit : ~13 rows (environ)
DELETE FROM `commande_produit`;
INSERT INTO `commande_produit` (`id`, `commande_id`, `produit_id`, `quantite`, `prix_unitaire`, `created_at`, `updated_at`) VALUES
	(9, 5, 2, 3, 2.00, '2025-04-10 10:55:53', '2025-04-10 10:55:53'),
	(10, 5, 3, 3, 23.44, '2025-04-10 10:55:53', '2025-04-10 10:55:53'),
	(13, 7, 1, 2, 78.00, '2025-04-10 11:00:22', '2025-04-10 11:00:22'),
	(14, 7, 2, 4, 2.00, '2025-04-10 11:00:22', '2025-04-10 11:00:22'),
	(43, 2, 1, 5, 78.00, '2025-04-14 12:16:22', '2025-04-14 12:16:22'),
	(44, 2, 1, 1, 78.00, '2025-04-14 12:16:22', '2025-04-14 12:16:22'),
	(45, 2, 4, 2, 22.00, '2025-04-14 12:16:22', '2025-04-14 12:16:22'),
	(50, 1, 1, 5, 78.00, '2025-04-14 13:29:14', '2025-04-14 13:29:14'),
	(51, 1, 2, 3, 2.00, '2025-04-14 13:29:14', '2025-04-14 13:29:14'),
	(52, 13, 2, 4, 2.00, '2025-04-16 10:58:23', '2025-04-16 10:58:23'),
	(53, 13, 4, 2, 22.00, '2025-04-16 10:58:23', '2025-04-16 10:58:23'),
	(54, 13, 1, 3, 78.00, '2025-04-16 10:58:23', '2025-04-16 10:58:23'),
	(55, 13, 3, 4, 23.44, '2025-04-16 10:58:23', '2025-04-16 10:58:23');

-- Listage de la structure de table infotools-new. commercials
CREATE TABLE IF NOT EXISTS `commercials` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `nom` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `telephone` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `adresse` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.commercials : ~2 rows (environ)
DELETE FROM `commercials`;
INSERT INTO `commercials` (`id`, `nom`, `email`, `telephone`, `adresse`, `created_at`, `updated_at`) VALUES
	(2, 'Coca cola', 'contact@cocacola.com', '07 78 00 28 20', '18 avenue de la cocaletérie', '2025-04-14 06:49:48', '2025-04-14 06:49:48'),
	(3, 'Kinder', 'contact@kinder.fr', '08 29 90 29 03', 'kinder glissant la pente de la chapelle rue 19, Bourges', '2025-04-16 11:18:31', '2025-04-16 11:18:31');

-- Listage de la structure de table infotools-new. failed_jobs
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

-- Listage des données de la table infotools-new.failed_jobs : ~0 rows (environ)
DELETE FROM `failed_jobs`;

-- Listage de la structure de table infotools-new. jobs
CREATE TABLE IF NOT EXISTS `jobs` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `queue` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `payload` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `attempts` tinyint unsigned NOT NULL,
  `reserved_at` int unsigned DEFAULT NULL,
  `available_at` int unsigned NOT NULL,
  `created_at` int unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `jobs_queue_index` (`queue`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.jobs : ~0 rows (environ)
DELETE FROM `jobs`;

-- Listage de la structure de table infotools-new. job_batches
CREATE TABLE IF NOT EXISTS `job_batches` (
  `id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `total_jobs` int NOT NULL,
  `pending_jobs` int NOT NULL,
  `failed_jobs` int NOT NULL,
  `failed_job_ids` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `options` mediumtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `cancelled_at` int DEFAULT NULL,
  `created_at` int NOT NULL,
  `finished_at` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.job_batches : ~0 rows (environ)
DELETE FROM `job_batches`;

-- Listage de la structure de table infotools-new. migrations
CREATE TABLE IF NOT EXISTS `migrations` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `migration` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `batch` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=63 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.migrations : ~12 rows (environ)
DELETE FROM `migrations`;
INSERT INTO `migrations` (`id`, `migration`, `batch`) VALUES
	(45, '0001_01_01_000000_create_users_table', 2),
	(46, '0001_01_01_000001_create_cache_table', 2),
	(47, '0001_01_01_000002_create_jobs_table', 2),
	(54, '2025_03_20_153050_create_clients_table', 3),
	(55, '2025_03_20_224446_create_produits_table', 4),
	(56, '2025_03_20_224532_create_rendez_vous_table', 5),
	(57, '2025_03_20_224710_create_commandes_table', 6),
	(58, '2025_03_20_224647_create_factures_table', 7),
	(59, '2025_03_20_224809_add_role_to_users_table', 8),
	(60, '2025_03_27_190206_create_personal_access_tokens_table', 9),
	(61, '2025_04_08_074100_create_prospects_table', 9),
	(62, '2025_04_09_114238_create_commercials_table', 10);

-- Listage de la structure de table infotools-new. password_reset_tokens
CREATE TABLE IF NOT EXISTS `password_reset_tokens` (
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `token` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.password_reset_tokens : ~0 rows (environ)
DELETE FROM `password_reset_tokens`;

-- Listage de la structure de table infotools-new. personal_access_tokens
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

-- Listage des données de la table infotools-new.personal_access_tokens : ~0 rows (environ)
DELETE FROM `personal_access_tokens`;
INSERT INTO `personal_access_tokens` (`id`, `tokenable_type`, `tokenable_id`, `name`, `token`, `abilities`, `last_used_at`, `expires_at`, `created_at`, `updated_at`) VALUES
	(1, 'App\\Models\\User', 1, 'myToken', '769f1424cb5e15ffa6c32e9b8a9d238655b8ed0140abf5b14b4f0a4110eff11e', '["*"]', '2025-04-16 12:34:56', NULL, '2025-04-15 07:05:27', '2025-04-16 12:34:56');

-- Listage de la structure de table infotools-new. produits
CREATE TABLE IF NOT EXISTS `produits` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `nom_produit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `prix` decimal(8,2) NOT NULL,
  `stock` int NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.produits : ~4 rows (environ)
DELETE FROM `produits`;
INSERT INTO `produits` (`id`, `nom_produit`, `description`, `prix`, `stock`, `created_at`, `updated_at`) VALUES
	(1, 'Serveur Web n°1', 'serveur web de 2 GO,\r\n2gb de ram', 2.50, 23, '2025-03-21 12:56:18', '2025-04-14 13:29:52'),
	(2, 'Serveur Web n°2', 'serveur web de 4 GO,\r\n4gb de ram', 6.00, 45, '2025-03-21 14:50:32', '2025-04-14 13:29:31'),
	(3, 'Serveur Web n°3', 'serveur web de 6 GO,\r\n6gb de ram', 12.50, 53, '2025-03-21 14:51:50', '2025-03-21 14:51:50'),
	(4, 'Serveur Web n°4', 'serveur web de 8 GO,\n8gb de ram', 16.20, 22, '2025-03-27 08:26:23', '2025-04-16 11:13:02');

-- Listage de la structure de table infotools-new. prospects
CREATE TABLE IF NOT EXISTS `prospects` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `nom` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `telephone` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `adresse` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.prospects : ~2 rows (environ)
DELETE FROM `prospects`;
INSERT INTO `prospects` (`id`, `nom`, `email`, `telephone`, `adresse`, `created_at`, `updated_at`) VALUES
	(3, 'Sofiane Mat', 'soso@gmail.com', '06 89 28 39 02', '18 ave de la saone', '2025-04-09 09:17:54', '2025-04-14 10:10:22'),
	(4, 'Andy Belin', 'andy@gmail.com', '07 39 20 82 93', '91 rue de l\'impase', '2025-04-09 09:18:29', '2025-04-14 10:10:33');

-- Listage de la structure de table infotools-new. rendez_vous
CREATE TABLE IF NOT EXISTS `rendez_vous` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `client_id` bigint unsigned NOT NULL,
  `commercial_id` bigint unsigned NOT NULL,
  `date_rendez_vous` date NOT NULL,
  `heure_rendez_vous` time NOT NULL,
  `description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `rendez_vous_client_id_foreign` (`client_id`),
  KEY `rendez_vous_commercial_id_foreign` (`commercial_id`),
  CONSTRAINT `rendez_vous_client_id_foreign` FOREIGN KEY (`client_id`) REFERENCES `clients` (`id`) ON DELETE CASCADE,
  CONSTRAINT `rendez_vous_commercial_id_foreign` FOREIGN KEY (`commercial_id`) REFERENCES `commercials` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.rendez_vous : ~2 rows (environ)
DELETE FROM `rendez_vous`;
INSERT INTO `rendez_vous` (`id`, `client_id`, `commercial_id`, `date_rendez_vous`, `heure_rendez_vous`, `description`, `created_at`, `updated_at`) VALUES
	(1, 2, 2, '2025-04-18', '19:29:00', 'Question sur les produits', '2025-04-16 12:29:24', '2025-04-16 12:41:49'),
	(2, 6, 3, '2025-05-01', '11:30:00', 'Demande d\'une visite de l\'entreprise', '2025-04-16 12:34:18', '2025-04-16 15:23:19');

-- Listage de la structure de table infotools-new. sessions
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

-- Listage des données de la table infotools-new.sessions : ~10 rows (environ)
DELETE FROM `sessions`;
INSERT INTO `sessions` (`id`, `user_id`, `ip_address`, `user_agent`, `payload`, `last_activity`) VALUES
	('62EXGxftEgDeCm4DA1ddvGpvUIAnqbJceghTkvpk', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Herd/1.18.0 Chrome/120.0.6099.291 Electron/28.2.5 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoiQUdlWVkxekhHNEhWR0pwaUVZZzE0Nlk5Y3NTUFFXcXNKdzF6a3JFMCI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6NDc6Imh0dHA6Ly9pbmZvdG9vbHMtbmV3LWxhcmF2ZWwudGVzdC8/aGVyZD1wcmV2aWV3Ijt9czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319fQ==', 1745521509),
	('9QWxLRDu9UBrpcQbG3r2r3mqcoD1wf3BKWN89U2S', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoiZDdRMHRBdm5Rb1FwZlFpbkFEWHB6U3drOFNkdTVoYzZLOXdqSHFkQiI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6Mzk6Imh0dHA6Ly9pbmZvdG9vbHMtbmV3LWxhcmF2ZWwudGVzdC9sb2dpbiI7fXM6NjoiX2ZsYXNoIjthOjI6e3M6Mzoib2xkIjthOjA6e31zOjM6Im5ldyI7YTowOnt9fX0=', 1746302447),
	('Byf55ctOztlbw5lbCqwNVbVEbPhfJOHa7TLwwOzf', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Herd/1.18.0 Chrome/120.0.6099.291 Electron/28.2.5 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoiTlJYc2d2aEV1d0JZUTFHTGM5RUhPd0lTWVdRNXdkSTB0aUVCY2hTbyI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6NDc6Imh0dHA6Ly9pbmZvdG9vbHMtbmV3LWxhcmF2ZWwudGVzdC8/aGVyZD1wcmV2aWV3Ijt9czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319fQ==', 1745513131),
	('cjcbGDc3Us3ikUFB8AwBDDLnX17cO7CE6IBO51HU', 1, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36', 'YTo0OntzOjY6Il90b2tlbiI7czo0MDoiNllNZ21VT0c2OVlwcVQ1QVBCbkhvQWVvbDA2dW5hemg0YlMzMkY1ZyI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6NDQ6Imh0dHBzOi8vaW5mb3Rvb2xzLW5ldy1sYXJhdmVsLnRlc3QvZGFzaGJvYXJkIjt9czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319czo1MDoibG9naW5fd2ViXzU5YmEzNmFkZGMyYjJmOTQwMTU4MGYwMTRjN2Y1OGVhNGUzMDk4OWQiO2k6MTt9', 1744824521),
	('dsxxbM4tQ0KvtsZCM8UBmJSWh7qufWA1xpGONn39', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Herd/1.18.0 Chrome/120.0.6099.291 Electron/28.2.5 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoiVmtmUWUydVo3dkVvajdEYzBOZFB3bjYyMmpNNDNuaUNGMVptaVdadSI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6NDc6Imh0dHA6Ly9pbmZvdG9vbHMtbmV3LWxhcmF2ZWwudGVzdC8/aGVyZD1wcmV2aWV3Ijt9czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319fQ==', 1745520461),
	('hqkS1ewVfyKUNCiJVls1O0B9uYAmGrSZZOrCSoXH', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Herd/1.18.0 Chrome/120.0.6099.291 Electron/28.2.5 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoid2d6cWliM3VzMXVIbllHZ1ZCaEdhaGlBNjcwemZxcHlOYXJzMDUyaiI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6NDc6Imh0dHA6Ly9pbmZvdG9vbHMtbmV3LWxhcmF2ZWwudGVzdC8/aGVyZD1wcmV2aWV3Ijt9czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319fQ==', 1746302436),
	('Rh3R9jNJcdIM3vWreI2GCfye90goTseMeEVH9atn', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Herd/1.18.0 Chrome/120.0.6099.291 Electron/28.2.5 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoiY0l4QVEwaDl2cFdpak9xck1BVGFDRHNIcmtnYTViTERRb3Q2c3JkayI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6NDc6Imh0dHA6Ly9pbmZvdG9vbHMtbmV3LWxhcmF2ZWwudGVzdC8/aGVyZD1wcmV2aWV3Ijt9czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319fQ==', 1745515408),
	('s1Yx6MCgdfJSbMb1FTtUQmDCbeARJMEf1pazRgRC', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Herd/1.18.0 Chrome/120.0.6099.291 Electron/28.2.5 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoiNlREUFJzY2xBQ2tWOHRPdVBQZWE0M1R3UUZDRG9nT0w3aXQ5WE5TeSI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6NDc6Imh0dHA6Ly9pbmZvdG9vbHMtbmV3LWxhcmF2ZWwudGVzdC8/aGVyZD1wcmV2aWV3Ijt9czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319fQ==', 1745513623),
	('sIWJlkyKPBkqDxmEjcqjsdiMUMWk5iMqYZ9jzPnv', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Herd/1.18.0 Chrome/120.0.6099.291 Electron/28.2.5 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoibkRaYXdvc2NPbGdsUnEzSkN5N1Q4dDMzcVlVQkt3SVlMWU1TYXpURiI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6NDc6Imh0dHA6Ly9pbmZvdG9vbHMtbmV3LWxhcmF2ZWwudGVzdC8/aGVyZD1wcmV2aWV3Ijt9czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319fQ==', 1745513131),
	('vmDdIblf7lrYnSKVtwd7yoKPYtvNf9vkdx3mkIYy', NULL, '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Herd/1.18.0 Chrome/120.0.6099.291 Electron/28.2.5 Safari/537.36', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoicURudGt6MHZuU3FQRmF1a2lCTXo5UVJQRlA4U21BbWJvZ211N004QyI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6NDc6Imh0dHA6Ly9pbmZvdG9vbHMtbmV3LWxhcmF2ZWwudGVzdC8/aGVyZD1wcmV2aWV3Ijt9czo2OiJfZmxhc2giO2E6Mjp7czozOiJvbGQiO2E6MDp7fXM6MzoibmV3IjthOjA6e319fQ==', 1745515412);

-- Listage de la structure de table infotools-new. users
CREATE TABLE IF NOT EXISTS `users` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `email_verified_at` timestamp NULL DEFAULT NULL,
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `role` enum('Responsable','Commercial') CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'Commercial',
  `remember_token` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `users_email_unique` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools-new.users : ~0 rows (environ)
DELETE FROM `users`;
INSERT INTO `users` (`id`, `name`, `email`, `email_verified_at`, `password`, `role`, `remember_token`, `created_at`, `updated_at`) VALUES
	(1, 'Andy Belin', 'ab@gmail.com', NULL, '$2y$12$AXOu7zCBmnNLdQn9xVjkD.WxsYi4BbJ6QetCsXbiuQmYeG4ev13Sa', 'Commercial', NULL, '2025-03-21 12:55:23', '2025-03-21 12:55:23');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
