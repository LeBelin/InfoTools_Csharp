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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.clients : ~0 rows (environ)
INSERT INTO `clients` (`idClient`, `nomClient`, `prenomClient`, `emailClient`, `telephoneClient`, `adresseClient`, `dateCreationClient`, `idCommerciaux`) VALUES
	(2, 'Andy', 'Belin', 'ab@gg', 43434, '18 ave', '12/05/2023', 1),
	(3, 'gfdg', 'fgdgd', 'gdgfdgfdgfd', 43434, 'gdgd', '22/05/2023', 4),
	(4, 'gfdg', 'fgdgd', 'gdgfdhfhgf', 43434, 'gdgd', '12/05/2023', 4);

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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.commerciaux : ~0 rows (environ)
INSERT INTO `commerciaux` (`idCommerciaux`, `nomCommerciaux`, `prenomCommerciaux`, `adresseCommerciaux`, `villeCommerciaux`, `cpCommerciaux`, `mailCommerciaux`, `telCommerciaux`) VALUES
	(1, 'Belin', 'Andy', '18 Avenueeee', 'Chalonnnn', 71101, 'ab@gmail.comm', 4343434),
	(4, 'gfdgdfg', 'DFFDFD', '18 Avenueeee', 'Chalonnnn', 71101, 'ab@gmail.com', 4343434);

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.factures : ~0 rows (environ)

-- Listage de la structure de table infotools. produit
CREATE TABLE IF NOT EXISTS `produit` (
  `idProduit` int NOT NULL AUTO_INCREMENT,
  `nomProduit` char(50) DEFAULT NULL,
  `desciptionProduit` text,
  `prixUnitaire` int DEFAULT NULL,
  `dateAjoutProduit` char(50) DEFAULT NULL,
  `imgProduit` char(50) DEFAULT NULL,
  PRIMARY KEY (`idProduit`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.produit : ~0 rows (environ)

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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.prospects : ~0 rows (environ)
INSERT INTO `prospects` (`idProspect`, `nomProspect`, `prenomProspect`, `telephoneProspect`, `emailProspect`, `dateCreation`, `idCommerciaux`) VALUES
	(2, 'Test', 'TEST', 434343, 'test@gmail.com', '15/06/2024', 4),
	(4, 'Andy', 'Belin', 644343, 'a@gmail.com', '19/04/2024', 1);

-- Listage de la structure de table infotools. rendezvous
CREATE TABLE IF NOT EXISTS `rendezvous` (
  `idRendezVous` int NOT NULL AUTO_INCREMENT,
  `dateRendezVous` char(50) DEFAULT NULL,
  `descriptionRendezVous` text,
  `heureDebutRendezVous` time DEFAULT NULL,
  `heureFintRendezVous` time DEFAULT NULL,
  `idCommerciaux` int NOT NULL,
  `idProspect` int NOT NULL,
  `idClient` int NOT NULL,
  PRIMARY KEY (`idRendezVous`),
  KEY `idCommerciaux` (`idCommerciaux`),
  KEY `idProspect` (`idProspect`),
  KEY `idClient` (`idClient`),
  CONSTRAINT `rendezvous_ibfk_1` FOREIGN KEY (`idCommerciaux`) REFERENCES `commerciaux` (`idCommerciaux`),
  CONSTRAINT `rendezvous_ibfk_2` FOREIGN KEY (`idProspect`) REFERENCES `prospects` (`idProspect`),
  CONSTRAINT `rendezvous_ibfk_3` FOREIGN KEY (`idClient`) REFERENCES `clients` (`idClient`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Listage des données de la table infotools.rendezvous : ~0 rows (environ)

-- Listage de la structure de table infotools. users
CREATE TABLE IF NOT EXISTS `users` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `email_verified_at` timestamp NULL DEFAULT NULL,
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `two_factor_secret` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `two_factor_recovery_codes` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `two_factor_confirmed_at` timestamp NULL DEFAULT NULL,
  `remember_token` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `current_team_id` int DEFAULT NULL,
  `profile_photo_path` varchar(2048) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `email` (`email`),
  UNIQUE KEY `users_email_unique` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Listage des données de la table infotools.users : ~1 rows (environ)
INSERT INTO `users` (`id`, `name`, `email`, `email_verified_at`, `password`, `two_factor_secret`, `two_factor_recovery_codes`, `two_factor_confirmed_at`, `remember_token`, `current_team_id`, `profile_photo_path`, `created_at`, `updated_at`) VALUES
	(1, 'Andy', 'ab@gmail.com', '2024-11-28 14:10:36', 'jsp', NULL, NULL, NULL, NULL, NULL, NULL, '2024-11-28 14:10:48', '2024-11-28 14:10:49');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
