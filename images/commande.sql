-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Client: 127.0.0.1
-- Généré le : Mer 18 Janvier 2012 à 22:42
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
-- Structure de la table `commande`
--

CREATE TABLE IF NOT EXISTS `commande` (
  `CDE_ID` int(11) NOT NULL DEFAULT '0',
  `CDE_NUM` varchar(10) DEFAULT NULL,
  `CDE_DATE` date DEFAULT NULL,
  `TRS_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`CDE_ID`),
  KEY `TRS_ID` (`TRS_ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

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

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
