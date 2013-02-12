-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Client: 127.0.0.1
-- Généré le : Mar 12 Février 2013 à 23:35
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
-- Structure de la table `adresse`
--

CREATE TABLE IF NOT EXISTS `adresse` (
  `ID_ADR` int(11) NOT NULL AUTO_INCREMENT,
  `NUM` int(6) DEFAULT NULL,
  `VOIE` varchar(200) DEFAULT NULL,
  `COMPLEMENT` varchar(200) DEFAULT NULL,
  `CP` varchar(100) DEFAULT NULL,
  `VILLE` varchar(100) DEFAULT NULL,
  `NUM_CHAMBRE` varchar(15) DEFAULT NULL,
  `IS_DELETED` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID_ADR`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=24 ;

--
-- Contenu de la table `adresse`
--

INSERT INTO `adresse` (`ID_ADR`, `NUM`, `VOIE`, `COMPLEMENT`, `CP`, `VILLE`, `NUM_CHAMBRE`, `IS_DELETED`) VALUES
(1, 26, 'Rue Bouquet', 'Logement 126', '77185', 'LOGNES', '1', 0),
(2, 1, 'Rue jean jaures', NULL, '75006', 'Paris', '', 0),
(3, 5, 'boulevard aussman', NULL, '75017', 'Paris', '', 0),
(4, 34, 'Rue toto', NULL, '75001', 'Paris', '', 0),
(5, 7, 'rue magique', NULL, '77420', 'Champs-Sur-Marne', '', 0),
(6, 8, 'rue toto', 'xwxwxcxw', '78001', 'Montesson', '', 0),
(7, 9, 'rue de paris', '', '93720', 'Montreuil', '1', 0),
(8, 9, 'rue de paris', NULL, NULL, 'Montreuil', '', 0),
(9, 9, 'rue de paris', NULL, NULL, 'Montreuil', '1', 0),
(10, 9, 'rue de paris', NULL, NULL, 'Montreuil', '1', 0),
(12, 8, 'rue toto', NULL, NULL, 'Montesson', '', 0),
(13, 26, 'Rue Bouquet', 'Logement 126', '77185', 'LOGNES', '14', 0),
(14, 26, 'Rue Bouquet', 'Logement 126', '77185', 'LOGNES', '12', 0),
(15, 23, 'Rue la hola', 'toto', '75690', 'Paris', NULL, 0),
(16, 12, 'Rue jaja', NULL, '77184', 'Paris', NULL, 0),
(17, 23, 'wxsqdqsdqsd', 'sdfsdfsdf', '77187', 'Paris', NULL, 0),
(18, 12, 'Rue bobo', NULL, '77187', 'Lognes', NULL, 0),
(19, 12, 'Rue bobo', NULL, '77187', 'Lognes', NULL, 0),
(20, 123, 'rue lala', NULL, '5547', 'Lognes', NULL, 0),
(21, 123, 'rue lala', NULL, '5547', 'Lognes', NULL, 0),
(22, 45, 'Rue la roche', 'Hotem Ibis', '75017', 'Paris', NULL, 0),
(23, 45, 'Rue la roche', 'Hotem Ibis', '75017', 'Paris', NULL, 0);

-- --------------------------------------------------------

--
-- Structure de la table `borne`
--

CREATE TABLE IF NOT EXISTS `borne` (
  `ID_BORN` int(11) NOT NULL AUTO_INCREMENT,
  `ID_ADR` int(11) DEFAULT NULL,
  `IS_ACTIF` tinyint(1) NOT NULL DEFAULT '1',
  `IS_OUVERT` tinyint(1) NOT NULL DEFAULT '1',
  `H_OUVERT_JOUR` datetime NOT NULL,
  `H_FERME_JOUR` datetime NOT NULL,
  `H_OUVERT_SOIR` datetime NOT NULL,
  `H_FERME_SOIR` datetime NOT NULL,
  `MESSAGE` varchar(500) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT '',
  `MESSAGE_INACTIF` varchar(500) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT '',
  `IS_DELETED` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID_BORN`),
  KEY `adresse_de_borne` (`ID_ADR`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=14 ;

--
-- Contenu de la table `borne`
--

INSERT INTO `borne` (`ID_BORN`, `ID_ADR`, `IS_ACTIF`, `IS_OUVERT`, `H_OUVERT_JOUR`, `H_FERME_JOUR`, `H_OUVERT_SOIR`, `H_FERME_SOIR`, `MESSAGE`, `MESSAGE_INACTIF`, `IS_DELETED`) VALUES
(1, 3, 1, 1, '2012-07-29 00:00:00', '2012-07-29 18:30:00', '2012-07-29 18:00:00', '2012-07-29 23:30:00', 'congé annuel', 'walo', 0),
(3, 10, 1, 1, '2012-07-29 00:00:00', '2012-07-29 18:30:00', '2012-07-29 18:00:00', '2012-07-29 23:30:00', 'congé annuel', '', 0),
(4, 12, 1, 1, '2012-07-29 00:00:00', '2012-07-29 18:30:00', '2012-07-29 18:00:00', '2012-07-29 23:30:00', 'congé annuel', '', 0),
(5, 12, 1, 1, '2012-07-29 00:00:00', '2012-07-29 18:30:00', '2012-07-29 18:00:00', '2012-07-29 23:30:00', 'congé annuel', '', 0),
(10, 12, 0, 1, '2012-07-29 00:00:00', '2012-07-29 18:30:00', '2012-07-29 18:00:00', '2012-07-29 23:30:00', 'congé annuel', NULL, 0),
(11, 14, 0, 1, '2012-07-29 00:00:00', '2012-07-29 18:30:00', '2012-07-29 18:00:00', '2012-07-29 23:30:00', 'congé annuel', NULL, 0),
(12, 18, 1, 1, '2012-07-29 00:00:00', '2012-07-29 18:30:00', '2012-07-29 18:00:00', '2012-07-29 23:30:00', 'congé annuel', 'rine', 0),
(13, 22, 1, 1, '0001-01-01 00:00:00', '0001-01-01 00:00:00', '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, 'Rien pour le moment', 0);

-- --------------------------------------------------------

--
-- Structure de la table `client`
--

CREATE TABLE IF NOT EXISTS `client` (
  `ID_CLIEN` int(11) NOT NULL AUTO_INCREMENT,
  `NOM` varchar(50) DEFAULT NULL,
  `PRENOM` varchar(50) DEFAULT NULL,
  `TEL` varchar(15) DEFAULT NULL,
  `EMAIL` varchar(100) DEFAULT NULL,
  `IS_DELETED` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID_CLIEN`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=11 ;

--
-- Contenu de la table `client`
--

INSERT INTO `client` (`ID_CLIEN`, `NOM`, `PRENOM`, `TEL`, `EMAIL`, `IS_DELETED`) VALUES
(1, 'chandarli', 'younes', '0664783697', 'younes@yahoo.fr', 0),
(2, 'mohi', 'toto', '0765345678', 'toto@gmail.fr', 0),
(3, 'rachid', 'rachid', '0654678965', 'rachid.gmail.com', 0),
(4, 'Masson', 'Damien', '0654678955', 'hola@jojo.fr', 0),
(6, 'Nom', 'Prenom', 'Tel', 'Email', 0),
(7, 'qwerty', 'Prenom', 'Tel', 'Email', 0),
(8, 'eeeeeeeee', 'tyui', 'ttyyuio', '', 0),
(9, '222222222', '¨¨¨oiiiiiiiiiiiiii', 'yhjjj', '', 0),
(10, 'toto', 'tata', '0A', '', 0);

-- --------------------------------------------------------

--
-- Structure de la table `commande`
--

CREATE TABLE IF NOT EXISTS `commande` (
  `NUM` int(11) NOT NULL AUTO_INCREMENT,
  `SOURCE` varchar(15) NOT NULL DEFAULT 'BORNE',
  `TOTAL` float NOT NULL DEFAULT '0',
  `TVA` float NOT NULL DEFAULT '0',
  `ID_ADR` int(11) DEFAULT NULL,
  `ID_CLIEN` int(11) DEFAULT NULL,
  `ID_BORN` int(11) DEFAULT NULL,
  `STATUT` varchar(20) NOT NULL DEFAULT 'ATTENTE',
  `DATE` datetime DEFAULT NULL,
  `ID_LIVREUR` int(11) DEFAULT NULL,
  `IS_DELETED` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`NUM`),
  KEY `adresse_de_commande` (`ID_ADR`),
  KEY `client_qui_commande` (`ID_CLIEN`),
  KEY `borne_qui_commande` (`ID_BORN`),
  KEY `livreur_de_commande` (`ID_LIVREUR`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=141 ;

--
-- Contenu de la table `commande`
--

INSERT INTO `commande` (`NUM`, `SOURCE`, `TOTAL`, `TVA`, `ID_ADR`, `ID_CLIEN`, `ID_BORN`, `STATUT`, `DATE`, `ID_LIVREUR`, `IS_DELETED`) VALUES
(87, 'TEST', 0, 0, 1, 1, 1, 'LIVREE', NULL, 2, 0),
(88, 'BORNE', 20.5, 1.2475, 1, 6, 1, 'LIVREE', '2012-07-30 14:40:11', 2, 0),
(89, 'BORNE', 16, 1.44, 1, 6, 1, 'LIVREE', '2012-07-30 14:53:20', 2, 1),
(90, 'BORNE', 23, 1.755, 1, 6, 1, 'LIVREE', '2012-07-30 14:58:43', 2, 0),
(91, 'BORNE', 97.5, 7.2225, 1, 6, 1, 'LIVREE', '2012-07-30 17:50:41', 2, 0),
(92, 'BORNE', 309, 21.61, 1, 6, 1, 'LIVREE', '2012-07-30 20:04:18', 2, 0),
(93, 'BORNE', 10, 0.55, 1, 6, 1, 'LIVREE', '2012-07-30 21:00:48', 2, 0),
(94, 'BORNE', 21.5, 1.5825, 1, 6, 1, 'LIVREE', '2012-07-31 08:30:28', 2, 0),
(95, 'BORNE', 15, 1.525, 1, 6, 1, 'LIVREE', '2012-07-31 08:37:27', 2, 0),
(96, 'BORNE', 381.5, 22.0325, 1, 6, 1, 'LIVREE', '2012-07-31 15:21:15', 2, 0),
(97, 'BORNE', 55, 3.405, 1, 6, 1, 'LIVREE', '2012-08-01 15:45:26', 6, 0),
(98, 'BORNE', 32.5, 1.7575, 1, 6, 1, 'ATTENTE', '2012-08-01 16:03:38', NULL, 0),
(99, 'BORNE', 72, 4.24, 1, 6, 1, 'PREPAREE', '2012-08-01 16:28:27', 6, 0),
(100, 'BORNE', 130, 7.53, 1, 6, 1, 'PRETE', '2012-08-01 21:42:04', 7, 0),
(101, 'BORNE', 39, 2.425, 1, 6, 1, 'ATTENTE', '2012-08-02 15:02:07', 2, 0),
(102, 'BORNE', 239, 13.705, 1, 7, 1, 'PRETE', '2012-08-02 18:05:01', 2, 0),
(103, 'BORNE', 1.5, 0.2925, 1, 6, 1, 'ATTENTE', '2012-08-02 18:22:53', 8, 0),
(104, 'BORNE', 23.5, 1.5025, 1, 6, 1, 'LIVREE', '2012-08-02 18:59:54', 2, 0),
(105, 'BORNE', 30, 1.65, 1, 8, 1, 'ATTENTE', '2012-09-01 16:22:44', 2, 0),
(106, 'BORNE', 10, 0.55, 1, 9, 1, 'ATTENTE', '2012-09-01 16:28:31', 2, 0),
(107, 'BORNE', 50, 2.75, 1, 6, 1, 'ATTENTE', '2012-09-01 16:37:35', NULL, 0),
(108, 'BORNE', 75, 4.125, 1, 6, 1, 'ATTENTE', '2012-09-01 16:38:44', 2, 0),
(109, 'BORNE', 105, 5.775, 1, 10, 1, 'ATTENTE', '2012-09-01 16:51:36', 2, 0),
(110, 'BORNE', 58, 3.75, 1, 6, 1, 'ATTENTE', '2012-09-01 17:27:15', 2, 0),
(111, 'BORNE', 88, 5.4, 1, 6, 1, 'ATTENTE', '2012-09-02 15:24:28', 2, 0),
(112, 'BORNE', 73, 4.575, 1, 10, 1, 'ATTENTE', '2012-09-02 15:38:16', 2, 0),
(113, 'TEST', 0, 0, 1, 1, 1, 'ATTENTE', NULL, 2, 0),
(114, 'BORNE', 94, 5.64, 1, 10, 1, 'ATTENTE', '2012-09-02 17:24:57', NULL, 0),
(115, 'BORNE', 88, 5.4, 1, 10, 1, 'ATTENTE', '2012-09-03 00:07:56', NULL, 0),
(116, 'BORNE', 36, 3.66, 1, 10, 1, 'ATTENTE', '2012-09-03 00:28:00', 2, 0),
(117, 'BORNE', 73, 4.575, 1, 10, 1, 'ATTENTE', '2012-09-03 01:01:41', NULL, 0),
(119, 'BORNE', 75, 4.725, 1, 10, 1, 'ATTENTE', '2012-09-03 08:38:46', NULL, 0),
(120, 'BORNE', 135, 8.325, 1, 10, 1, 'ATTENTE', '2012-09-03 08:51:42', NULL, 0),
(121, 'BORNE', 34.5, 3.1275, 1, 10, 1, 'ATTENTE', '2012-09-03 09:16:32', NULL, 0),
(123, 'BORNE', 11.5, 2.2425, 1, 10, 1, 'ATTENTE', '2013-02-07 19:29:08', NULL, 0),
(125, 'BORNE', 34.5, 2.4125, 1, 10, 1, 'ATTENTE', '2013-02-07 21:50:27', NULL, 0),
(126, 'BORNE', 19, 2.025, 1, 10, 1, 'ATTENTE', '2013-02-07 22:16:36', NULL, 0),
(127, 'BORNE', 17.5, 1.7325, 1, 10, 1, 'ATTENTE', '2013-02-07 22:20:27', NULL, 0),
(128, 'BORNE', 17.5, 1.7325, 1, 10, 1, 'ATTENTE', '2013-02-07 22:48:34', NULL, 0),
(129, 'BORNE', 16, 1.415, 1, 10, 1, 'ATTENTE', '2013-02-07 22:59:09', NULL, 0),
(130, 'BORNE', 20.5, 1.5425, 1, 10, 1, 'ATTENTE', '2013-02-07 23:20:39', NULL, 0),
(131, 'BORNE', 67.5, 4.4825, 1, 10, 1, 'ATTENTE', '2013-02-08 20:58:39', NULL, 0),
(132, 'BORNE', 227, 31.365, 1, 10, 1, 'ATTENTE', '2013-02-08 23:25:31', NULL, 0),
(133, 'BORNE', 50, 2.75, 1, 10, 1, 'ATTENTE', '2013-02-08 22:47:10', NULL, 0),
(134, 'BORNE', 17.5, 1.7325, 1, 10, 1, 'ATTENTE', '2013-02-09 09:16:17', NULL, 0),
(135, 'BORNE', 28.5, 2.1725, 1, 10, 1, 'ATTENTE', '2013-02-09 09:34:14', NULL, 0),
(136, 'BORNE', 39, 2.425, 1, 10, 1, 'ATTENTE', '2013-02-09 09:39:13', NULL, 0),
(137, 'BORNE', 20.5, 1.5425, 1, 10, 1, 'ATTENTE', '2013-02-09 10:35:44', NULL, 0),
(138, 'BORNE', 25, 1.49, 1, 10, 1, 'ATTENTE', '2013-02-09 10:37:07', NULL, 0),
(139, 'BORNE', 67.5, 4.4825, 1, 10, 1, 'ATTENTE', '2013-02-09 10:54:44', NULL, 0),
(140, 'BORNE', 80, 4.4, 3, 10, 1, 'ATTENTE', '2013-02-10 13:07:23', NULL, 0);

-- --------------------------------------------------------

--
-- Structure de la table `famille`
--

CREATE TABLE IF NOT EXISTS `famille` (
  `ID_FAMIL` int(11) NOT NULL AUTO_INCREMENT,
  `NOM` varchar(100) DEFAULT NULL,
  `IMAGE_URL` varchar(100) DEFAULT NULL,
  `DESCRIPTION` varchar(150) NOT NULL,
  `TVA` float NOT NULL DEFAULT '0',
  `IS_APP` tinyint(1) NOT NULL DEFAULT '1',
  `IS_WEB` tinyint(1) NOT NULL DEFAULT '1',
  `IS_ACTIF` tinyint(1) NOT NULL DEFAULT '1',
  `IS_DELETED` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID_FAMIL`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=16 ;

--
-- Contenu de la table `famille`
--

INSERT INTO `famille` (`ID_FAMIL`, `NOM`, `IMAGE_URL`, `DESCRIPTION`, `TVA`, `IS_APP`, `IS_WEB`, `IS_ACTIF`, `IS_DELETED`) VALUES
(2, 'Pizzas', 'pizzas.png', 'PZ', 5.5, 1, 1, 1, 0),
(3, 'tex-mexs', 'tex-mexs.png', '', 4, 1, 1, 1, 0),
(4, 'menus', 'menus.png', '', 7.5, 1, 1, 1, 0),
(5, 'salades', 'salades.png', '', 19.6, 1, 1, 1, 0),
(7, 'Crepes', 'Penguins.jpg', 'Crepes', 10, 1, 0, 1, 0),
(12, 'Fruits', 'Koala.jpg', 'fruits desc', 10, 1, 1, 1, 0),
(13, 'Boissons', 'boissons.png', '', 5.5, 1, 1, 1, 1),
(14, 'Glaces', 'Penguins.jpg', 'Bonnes', 19, 1, 1, 1, 0),
(15, 'Boissons', 'boissons.png', 'Boire et mourir', 5.5, 1, 1, 1, 0);

-- --------------------------------------------------------

--
-- Structure de la table `langue`
--

CREATE TABLE IF NOT EXISTS `langue` (
  `ID_LANG` int(11) NOT NULL AUTO_INCREMENT,
  `NOM` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `CODE` varchar(5) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `IS_DELETED` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID_LANG`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Contenu de la table `langue`
--

INSERT INTO `langue` (`ID_LANG`, `NOM`, `CODE`, `IS_DELETED`) VALUES
(1, 'French', 'FR', 0),
(2, 'English', 'EN', 0),
(3, 'Español', 'SP', 0);

-- --------------------------------------------------------

--
-- Structure de la table `livreur`
--

CREATE TABLE IF NOT EXISTS `livreur` (
  `ID_LIVREUR` int(11) NOT NULL AUTO_INCREMENT,
  `NOM` varchar(50) NOT NULL,
  `PRENOM` varchar(50) NOT NULL,
  `IS_DELETED` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID_LIVREUR`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=9 ;

--
-- Contenu de la table `livreur`
--

INSERT INTO `livreur` (`ID_LIVREUR`, `NOM`, `PRENOM`, `IS_DELETED`) VALUES
(2, 'lolo', 'lplp', 1),
(6, 'Mickel', 'Polo', 1),
(7, 'Younes', 'yonooos', 1),
(8, 'Partick', 'Zlolo', 0);

-- --------------------------------------------------------

--
-- Structure de la table `menu`
--

CREATE TABLE IF NOT EXISTS `menu` (
  `ID_MENU` int(11) NOT NULL AUTO_INCREMENT,
  `ID_PROD` int(11) NOT NULL,
  `NUM_COM` int(11) NOT NULL,
  `QUANTITE` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ID_MENU`),
  KEY `menu` (`ID_PROD`),
  KEY `commande_du_menu` (`NUM_COM`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=42 ;

--
-- Contenu de la table `menu`
--

INSERT INTO `menu` (`ID_MENU`, `ID_PROD`, `NUM_COM`, `QUANTITE`) VALUES
(1, 9, 87, 1),
(2, 10, 89, 1),
(3, 9, 90, 1),
(4, 10, 91, 1),
(5, 9, 91, 1),
(6, 10, 92, 1),
(7, 9, 92, 1),
(8, 10, 94, 1),
(9, 10, 96, 1),
(10, 10, 96, 1),
(11, 10, 97, 1),
(12, 10, 99, 1),
(13, 10, 100, 1),
(14, 10, 101, 1),
(15, 10, 102, 1),
(16, 10, 110, 1),
(17, 10, 111, 1),
(18, 10, 112, 1),
(19, 9, 113, 0),
(20, 10, 114, 2),
(21, 10, 115, 2),
(22, 10, 116, 2),
(23, 10, 117, 2),
(26, 9, 119, 2),
(27, 9, 120, 3),
(28, 9, 121, 2),
(29, 10, 125, 1),
(30, 10, 126, 1),
(31, 10, 127, 1),
(32, 10, 128, 1),
(33, 10, 130, 1),
(34, 10, 131, 1),
(35, 10, 132, 1),
(36, 10, 134, 1),
(37, 10, 135, 1),
(38, 10, 136, 1),
(39, 10, 137, 1),
(40, 10, 138, 1),
(41, 10, 139, 1);

-- --------------------------------------------------------

--
-- Structure de la table `produit`
--

CREATE TABLE IF NOT EXISTS `produit` (
  `ID_PROD` int(11) NOT NULL AUTO_INCREMENT,
  `TVA` float NOT NULL DEFAULT '0',
  `NOM` varchar(50) DEFAULT NULL,
  `IMAGE_URL` varchar(100) DEFAULT NULL,
  `DESCRIPTION` varchar(200) DEFAULT NULL,
  `PRIX` float NOT NULL DEFAULT '0',
  `ID_FAMIL` int(11) DEFAULT NULL,
  `IS_APP` tinyint(1) NOT NULL DEFAULT '1',
  `IS_WEB` tinyint(1) NOT NULL DEFAULT '1',
  `IS_ACTIF` tinyint(1) NOT NULL DEFAULT '1',
  `IS_DELETED` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID_PROD`),
  KEY `famille_de_produit` (`ID_FAMIL`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=27 ;

--
-- Contenu de la table `produit`
--

INSERT INTO `produit` (`ID_PROD`, `TVA`, `NOM`, `IMAGE_URL`, `DESCRIPTION`, `PRIX`, `ID_FAMIL`, `IS_APP`, `IS_WEB`, `IS_ACTIF`, `IS_DELETED`) VALUES
(3, 0, 'pizza match', '00014162.jpg', 'hoho', 15, 2, 1, 1, 1, 1),
(5, 0, 'oasis', 'oasis.png', '33 cl', 2, 15, 1, 1, 1, 0),
(6, 0, 'pizza margarita', 'margarita.png', NULL, 10, 2, 1, 1, 1, 0),
(7, 0, 'pizza 4 fromages', 'pizza_4_fromages.png', NULL, 12, 2, 1, 1, 1, 0),
(8, 0, 'pizza végétarienne', 'vegetarienne.png', NULL, 13, 2, 1, 1, 1, 0),
(9, 0, 'menu 1', 'Penguins.jpg', 'menu1_description', 15, 4, 1, 1, 1, 1),
(10, 0, 'menu 2', 'menu2.png', NULL, 14, 4, 1, 1, 1, 0),
(11, 0, 'chiken 1', 'chiken1.png', NULL, 5, 3, 1, 1, 1, 0),
(12, 0, 'chiken 2', 'chiken2.png', NULL, 6, 3, 1, 1, 1, 0),
(16, 0, 'salade 1', 'result.png', 'saladistes', 10, 5, 1, 1, 1, 0),
(17, 0, 'salade alzacienne', 'Chrysanthemum.jpg', 'rien de spéciale dqsdqsd qsdqsdqsd qsdqsdqsd qsdq', 50, 5, 1, 1, 1, 0),
(19, 0, 'fromage', 'Desert.jpg', 'blanc', 10, 12, 1, 1, 1, 0),
(20, 0, 'A', 'Desert.jpg', 'Dec B', 20, 14, 1, 1, 0, 0),
(21, 0, 'Menu spécial', 'Tulips.jpg', 'vraiment spécial', 7, 4, 1, 1, 1, 0),
(22, 0, 'BBBBB', 'w8bg.png', 'TTTTTT', 60, 5, 1, 1, 1, 0),
(23, 0, 'Macdo', 'n77vx9qjyhbepyk3lruw.jpg', '', 7, 4, 1, 1, 1, 0),
(24, 0, 'Produits toto', 'Hydrangeas.jpg', 'juste pour un test', 10, NULL, 1, 1, 1, 1),
(25, 0, 'Coca', 'cocacola.png', 'Fraiche', 3, 15, 1, 1, 1, 0),
(26, 0, 'Menu maison', '00014162.jpg', 'Fait maison', 5, 4, 1, 1, 1, 0);

-- --------------------------------------------------------

--
-- Structure de la table `produits_commande`
--

CREATE TABLE IF NOT EXISTS `produits_commande` (
  `NUM_COM` int(11) NOT NULL,
  `ID_PROD` int(11) NOT NULL,
  `QUANTITE` int(6) NOT NULL,
  PRIMARY KEY (`NUM_COM`,`ID_PROD`),
  KEY `commande_produits` (`NUM_COM`),
  KEY `produits_commande` (`ID_PROD`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `produits_commande`
--

INSERT INTO `produits_commande` (`NUM_COM`, `ID_PROD`, `QUANTITE`) VALUES
(87, 3, 0),
(88, 8, 1),
(88, 12, 1),
(89, 5, 1),
(90, 5, 1),
(90, 12, 1),
(91, 3, 3),
(91, 5, 2),
(91, 6, 1),
(91, 7, 1),
(91, 8, 1),
(91, 11, 2),
(91, 12, 2),
(92, 3, 2),
(92, 5, 12),
(92, 6, 3),
(92, 7, 12),
(92, 8, 5),
(92, 11, 1),
(92, 12, 1),
(93, 6, 1),
(94, 12, 1),
(95, 3, 1),
(95, 5, 1),
(95, 6, 1),
(96, 3, 21),
(96, 5, 1),
(96, 6, 1),
(96, 7, 1),
(96, 8, 1),
(97, 3, 1),
(97, 5, 1),
(97, 7, 1),
(97, 12, 2),
(98, 3, 1),
(98, 11, 2),
(98, 12, 1),
(99, 3, 1),
(99, 6, 3),
(99, 8, 1),
(100, 3, 4),
(100, 5, 1),
(100, 6, 3),
(100, 7, 1),
(100, 12, 2),
(101, 3, 1),
(101, 6, 1),
(102, 3, 3),
(102, 5, 1),
(102, 6, 1),
(102, 7, 1),
(102, 8, 12),
(104, 6, 1),
(104, 7, 1),
(105, 3, 2),
(106, 6, 1),
(107, 3, 1),
(107, 6, 1),
(107, 7, 1),
(107, 8, 1),
(108, 3, 5),
(109, 3, 7),
(110, 3, 2),
(111, 3, 4),
(112, 3, 3),
(113, 3, 0),
(114, 3, 4),
(114, 12, 1),
(115, 3, 4),
(116, 5, 4),
(117, 3, 3),
(119, 3, 3),
(120, 3, 4),
(120, 6, 3),
(123, 5, 5),
(125, 5, 1),
(125, 11, 1),
(125, 12, 2),
(126, 5, 1),
(127, 5, 1),
(128, 5, 1),
(129, 5, 1),
(129, 11, 1),
(129, 12, 1),
(130, 11, 1),
(131, 3, 1),
(131, 5, 1),
(131, 6, 1),
(131, 7, 1),
(131, 8, 1),
(132, 3, 1),
(132, 5, 6),
(132, 6, 1),
(132, 7, 1),
(132, 8, 1),
(132, 11, 2),
(132, 12, 3),
(132, 16, 2),
(132, 17, 2),
(133, 3, 1),
(133, 6, 1),
(133, 7, 1),
(133, 8, 1),
(134, 5, 1),
(135, 5, 1),
(135, 11, 1),
(135, 12, 1),
(136, 3, 1),
(136, 6, 1),
(137, 11, 1),
(138, 11, 1),
(138, 12, 1),
(139, 3, 1),
(139, 5, 1),
(139, 6, 1),
(139, 7, 1),
(139, 8, 1),
(140, 6, 8);

-- --------------------------------------------------------

--
-- Structure de la table `produits_menu`
--

CREATE TABLE IF NOT EXISTS `produits_menu` (
  `ID_MENU` int(11) NOT NULL,
  `ID_PROD` int(11) NOT NULL,
  `QUANTITE` int(6) NOT NULL,
  PRIMARY KEY (`ID_PROD`,`ID_MENU`),
  KEY `produits_menu` (`ID_PROD`),
  KEY `menu_produits` (`ID_MENU`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `produits_menu`
--

INSERT INTO `produits_menu` (`ID_MENU`, `ID_PROD`, `QUANTITE`) VALUES
(29, 3, 1),
(30, 3, 1),
(31, 3, 1),
(32, 3, 2),
(33, 3, 1),
(35, 3, 1),
(36, 3, 1),
(40, 3, 1),
(10, 5, 1),
(11, 5, 1),
(13, 5, 1),
(14, 5, 1),
(15, 5, 1),
(16, 5, 1),
(17, 5, 1),
(18, 5, 1),
(20, 5, 1),
(21, 5, 1),
(22, 5, 1),
(23, 5, 1),
(27, 5, 3),
(28, 5, 2),
(2, 6, 2),
(6, 6, 2),
(7, 6, 1),
(20, 6, 2),
(21, 6, 2),
(22, 6, 1),
(29, 6, 1),
(30, 6, 1),
(31, 6, 1),
(33, 6, 1),
(34, 6, 2),
(35, 6, 1),
(36, 6, 1),
(37, 6, 2),
(38, 6, 2),
(39, 6, 2),
(40, 6, 1),
(41, 6, 1),
(3, 7, 1),
(5, 7, 1),
(10, 7, 2),
(23, 7, 1),
(27, 7, 3),
(28, 7, 2),
(41, 7, 1),
(4, 8, 2),
(8, 8, 2),
(9, 8, 2),
(11, 8, 2),
(12, 8, 2),
(13, 8, 2),
(14, 8, 2),
(15, 8, 2),
(16, 8, 2),
(17, 8, 2),
(18, 8, 2),
(21, 8, 2),
(22, 8, 1),
(23, 8, 1),
(26, 8, 2),
(5, 11, 1),
(7, 11, 1),
(27, 11, 3),
(28, 11, 2),
(3, 12, 1),
(26, 12, 2),
(26, 16, 10),
(27, 16, 15),
(28, 16, 10);

-- --------------------------------------------------------

--
-- Structure de la table `produit_composition`
--

CREATE TABLE IF NOT EXISTS `produit_composition` (
  `ID_FAMIL` int(11) NOT NULL,
  `ID_PROD` int(11) NOT NULL,
  `QUANTITE` int(11) DEFAULT NULL,
  `IS_MEME` tinyint(1) NOT NULL,
  PRIMARY KEY (`ID_FAMIL`,`ID_PROD`),
  KEY `familles_produit` (`ID_FAMIL`),
  KEY `produits_famille` (`ID_PROD`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `produit_composition`
--

INSERT INTO `produit_composition` (`ID_FAMIL`, `ID_PROD`, `QUANTITE`, `IS_MEME`) VALUES
(2, 9, 3, 1),
(2, 10, 1, 1),
(2, 21, 1, 1),
(2, 26, 2, 0),
(3, 9, 1, 1),
(3, 23, 7, 1),
(3, 26, 5, 1),
(5, 9, 5, 1),
(7, 9, 3, 1),
(7, 26, 5, 1),
(12, 9, 5, 0),
(13, 23, 1, 1),
(15, 10, 2, 0);

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `borne`
--
ALTER TABLE `borne`
  ADD CONSTRAINT `adresse_de_borne` FOREIGN KEY (`ID_ADR`) REFERENCES `adresse` (`ID_ADR`);

--
-- Contraintes pour la table `commande`
--
ALTER TABLE `commande`
  ADD CONSTRAINT `adresse_de_commande` FOREIGN KEY (`ID_ADR`) REFERENCES `adresse` (`ID_ADR`),
  ADD CONSTRAINT `borne_qui_commande` FOREIGN KEY (`ID_BORN`) REFERENCES `borne` (`ID_BORN`),
  ADD CONSTRAINT `client_qui_commande` FOREIGN KEY (`ID_CLIEN`) REFERENCES `client` (`ID_CLIEN`),
  ADD CONSTRAINT `livreur_de_commande` FOREIGN KEY (`ID_LIVREUR`) REFERENCES `livreur` (`ID_LIVREUR`);

--
-- Contraintes pour la table `menu`
--
ALTER TABLE `menu`
  ADD CONSTRAINT `commande_du_menu` FOREIGN KEY (`NUM_COM`) REFERENCES `commande` (`NUM`),
  ADD CONSTRAINT `menu` FOREIGN KEY (`ID_PROD`) REFERENCES `produit` (`ID_PROD`);

--
-- Contraintes pour la table `produit`
--
ALTER TABLE `produit`
  ADD CONSTRAINT `famille_de_produit` FOREIGN KEY (`ID_FAMIL`) REFERENCES `famille` (`ID_FAMIL`);

--
-- Contraintes pour la table `produits_commande`
--
ALTER TABLE `produits_commande`
  ADD CONSTRAINT `commande_produits` FOREIGN KEY (`NUM_COM`) REFERENCES `commande` (`NUM`),
  ADD CONSTRAINT `produits_commande` FOREIGN KEY (`ID_PROD`) REFERENCES `produit` (`ID_PROD`);

--
-- Contraintes pour la table `produits_menu`
--
ALTER TABLE `produits_menu`
  ADD CONSTRAINT `menu_produits` FOREIGN KEY (`ID_MENU`) REFERENCES `menu` (`ID_MENU`),
  ADD CONSTRAINT `produits_menu` FOREIGN KEY (`ID_PROD`) REFERENCES `produit` (`ID_PROD`);

--
-- Contraintes pour la table `produit_composition`
--
ALTER TABLE `produit_composition`
  ADD CONSTRAINT `familles_produit` FOREIGN KEY (`ID_FAMIL`) REFERENCES `famille` (`ID_FAMIL`),
  ADD CONSTRAINT `produits_famille` FOREIGN KEY (`ID_PROD`) REFERENCES `produit` (`ID_PROD`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
