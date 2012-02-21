-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Client: 127.0.0.1
-- Généré le : Mer 18 Janvier 2012 à 23:08
-- Version du serveur: 5.5.16
-- Version de PHP: 5.3.8

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données: `denapoli`
--

-- --------------------------------------------------------

--
-- Structure de la table `article`
--

CREATE TABLE IF NOT EXISTS `article` (
  `ART_ID` int(11) NOT NULL DEFAULT '0',
  `ART_LIBELLE` varchar(20) DEFAULT NULL,
  `ART_PRIXBASEHT` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`ART_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `article`
--

INSERT INTO `article` (`ART_ID`, `ART_LIBELLE`, `ART_PRIXBASEHT`) VALUES
(1, 'IPOD', '149'),
(2, 'XBOX360', '234'),
(3, 'PSP3', '399'),
(4, 'PSP', '150'),
(5, 'WII', '200');

-- --------------------------------------------------------

--
-- Structure de la table `commande`
--

CREATE TABLE IF NOT EXISTS `commande` (
  `CDE_ID` int(11) NOT NULL DEFAULT '0',
  `CDE_NUM` varchar(10) DEFAULT NULL,
  `CDE_DATE` date DEFAULT NULL,
  `TRS_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`CDE_ID`),
  KEY `TRS_ID` (`TRS_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `commande`
--

INSERT INTO `commande` (`CDE_ID`, `CDE_NUM`, `CDE_DATE`, `TRS_ID`) VALUES
(1, 'CDE 1', '2009-01-02', 1),
(2, 'CDE 2', '2009-01-08', 1),
(3, 'CDE 3', '2009-01-13', 1),
(4, 'CDE 4', '2009-01-06', 2),
(5, 'CDE 5', '2009-01-21', 2),
(6, 'CDE 6', '2009-02-04', 3);

-- --------------------------------------------------------

--
-- Structure de la table `lignes`
--

CREATE TABLE IF NOT EXISTS `lignes` (
  `LGN_ID` int(11) NOT NULL DEFAULT '0',
  `LGN_QTE` int(11) DEFAULT NULL,
  `LGN_TOTALHT` decimal(10,0) DEFAULT NULL,
  `CDE_ID` int(11) DEFAULT NULL,
  `ART_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`LGN_ID`),
  KEY `CDE` (`CDE_ID`),
  KEY `ART` (`ART_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `lignes`
--

INSERT INTO `lignes` (`LGN_ID`, `LGN_QTE`, `LGN_TOTALHT`, `CDE_ID`, `ART_ID`) VALUES
(1, 2, '468', 1, 2),
(2, 10, '3990', 1, 3),
(3, 5, '745', 2, 1),
(4, 2, '798', 3, 3),
(5, 15, '2250', 3, 4),
(6, 1, '200', 3, 5),
(7, 1, '200', 4, 5),
(8, 10, '2340', 5, 2),
(9, 6, '894', 6, 1),
(10, 10, '1500', 6, 5);

-- --------------------------------------------------------

--
-- Structure de la table `tiers`
--

CREATE TABLE IF NOT EXISTS `tiers` (
  `TRS_ID` int(11) NOT NULL DEFAULT '0',
  `TRS_NOM` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`TRS_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `tiers`
--

INSERT INTO `tiers` (`TRS_ID`, `TRS_NOM`) VALUES
(1, 'JASKULA'),
(2, 'NODEVO'),
(3, 'DUPONT');

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `commande`
--
ALTER TABLE `commande`
  ADD CONSTRAINT `commande_ibfk_1` FOREIGN KEY (`TRS_ID`) REFERENCES `tiers` (`TRS_ID`);

--
-- Contraintes pour la table `lignes`
--
ALTER TABLE `lignes`
  ADD CONSTRAINT `ART` FOREIGN KEY (`ART_ID`) REFERENCES `article` (`ART_ID`),
  ADD CONSTRAINT `CDE` FOREIGN KEY (`CDE_ID`) REFERENCES `commande` (`CDE_ID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
