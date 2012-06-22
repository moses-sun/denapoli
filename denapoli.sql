-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Client: 127.0.0.1
-- Généré le : Sam 23 Juin 2012 à 01:05
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
  PRIMARY KEY (`ID_ADR`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=13 ;

--
-- Contenu de la table `adresse`
--

INSERT INTO `adresse` (`ID_ADR`, `NUM`, `VOIE`, `COMPLEMENT`, `CP`, `VILLE`, `NUM_CHAMBRE`) VALUES
(1, 26, 'Rue Bouquet', 'Logement 126', '77185', 'LOGNES', '25'),
(2, 1, 'Rue jean jaures', NULL, '75006', 'Paris', ''),
(3, 5, 'boulevard aussman', NULL, '75017', 'Paris', ''),
(4, 34, 'Rue toto', NULL, '75001', 'Paris', ''),
(5, 7, 'rue magique', NULL, '77420', 'Champs-Sur-Marne', ''),
(6, 8, 'rue toto', 'xwxwxcxw', '78001', 'Montesson', ''),
(7, 9, 'rue de paris', '', '93720', 'Montreuil', '1'),
(8, 9, 'rue de paris', NULL, NULL, 'Montreuil', ''),
(9, 9, 'rue de paris', NULL, NULL, 'Montreuil', '1'),
(10, 9, 'rue de paris', NULL, NULL, 'Montreuil', '1'),
(12, 8, 'rue toto', NULL, NULL, 'Montesson', '');

-- --------------------------------------------------------

--
-- Structure de la table `borne`
--

CREATE TABLE IF NOT EXISTS `borne` (
  `ID_BORN` int(11) NOT NULL AUTO_INCREMENT,
  `ID_ADR` int(11) NOT NULL,
  PRIMARY KEY (`ID_BORN`),
  KEY `adresse_de_borne` (`ID_ADR`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Contenu de la table `borne`
--

INSERT INTO `borne` (`ID_BORN`, `ID_ADR`) VALUES
(2, 6),
(1, 7),
(3, 10);

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
  PRIMARY KEY (`ID_CLIEN`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Contenu de la table `client`
--

INSERT INTO `client` (`ID_CLIEN`, `NOM`, `PRENOM`, `TEL`, `EMAIL`) VALUES
(1, 'chandarli', 'younes', '0664783697', 'younes@yahoo.fr'),
(2, 'mohi', 'toto', '0765345678', 'toto@gmail.fr'),
(3, 'rachid', 'rachid', '0654678965', 'rachid.gmail.com'),
(4, 'Masson', 'Damien', '0654678955', 'hola@jojo.fr'),
(6, 'Nom', 'Prenom', 'Tel', 'Email');

-- --------------------------------------------------------

--
-- Structure de la table `commande`
--

CREATE TABLE IF NOT EXISTS `commande` (
  `NUM` int(11) NOT NULL AUTO_INCREMENT,
  `TOTAL` float NOT NULL DEFAULT '0',
  `ID_ADR` int(11) NOT NULL,
  `ID_CLIEN` int(11) NOT NULL,
  `ID_BORN` int(11) NOT NULL,
  `STATUT` varchar(20) NOT NULL DEFAULT 'ATTENTE',
  `DATE` datetime DEFAULT NULL,
  `ID_LIVREUR` int(11) DEFAULT NULL,
  PRIMARY KEY (`NUM`),
  KEY `adresse_de_commande` (`ID_ADR`),
  KEY `client_qui_commande` (`ID_CLIEN`),
  KEY `borne_qui_commande` (`ID_BORN`),
  KEY `livreur_de_commande` (`ID_LIVREUR`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=66 ;

--
-- Contenu de la table `commande`
--

INSERT INTO `commande` (`NUM`, `TOTAL`, `ID_ADR`, `ID_CLIEN`, `ID_BORN`, `STATUT`, `DATE`, `ID_LIVREUR`) VALUES
(1, 0, 1, 1, 1, 'LIVREE', '2012-04-22 22:55:56', 2),
(2, 0, 2, 2, 2, 'ATTENTE', '2012-04-22 22:55:56', 2),
(3, 0, 3, 3, 1, 'LIVREE', '2012-04-22 22:55:56', 1),
(4, 0, 4, 2, 2, 'PREPAREE', '2012-04-22 22:55:56', 1),
(7, 0, 1, 1, 2, 'ATTENTE', '2012-04-22 22:55:56', 1),
(8, 0, 1, 1, 2, 'ATTENTE', '2012-04-22 22:55:56', 2),
(9, 0, 1, 1, 2, 'ATTENTE', '2012-04-22 22:55:56', 1),
(10, 0, 1, 1, 2, 'ATTENTE', '2012-04-22 22:55:56', 2),
(11, 16.5, 1, 2, 1, 'ATTENTE', '2012-04-22 22:55:56', 2),
(14, 0, 1, 1, 2, 'ATTENTE', '2012-04-22 22:55:56', 1),
(15, 0, 7, 6, 1, 'ATTENTE', '2012-04-22 22:55:56', 1),
(16, 1.5, 7, 6, 1, 'ATTENTE', '2012-04-22 22:55:56', 2),
(17, 0, 7, 6, 1, 'ATTENTE', '2012-04-22 22:55:56', 1),
(18, 0, 7, 6, 1, 'ATTENTE', '2012-04-22 23:14:14', NULL),
(20, 0, 7, 6, 1, 'ATTENTE', '2012-04-22 23:16:21', NULL),
(22, 0, 7, 6, 1, 'ATTENTE', '2012-04-22 23:21:40', NULL),
(23, 0, 7, 6, 1, 'ATTENTE', '2012-04-24 22:46:06', NULL),
(25, 0, 7, 6, 1, 'ATTENTE', '2012-04-24 22:52:44', NULL),
(27, 0, 7, 6, 1, 'ATTENTE', '2012-04-24 23:00:44', NULL),
(29, 0, 1, 1, 2, 'ATTENTE', NULL, NULL),
(30, 0, 1, 1, 2, 'ATTENTE', NULL, NULL),
(31, 0, 7, 6, 1, 'ATTENTE', '2012-06-21 23:22:34', NULL),
(32, 0, 7, 6, 1, 'ATTENTE', '2012-06-21 23:27:37', NULL),
(33, 21.5, 7, 6, 1, 'ATTENTE', '2012-06-21 23:27:37', NULL),
(34, 0, 7, 6, 1, 'ATTENTE', '2012-06-22 22:11:08', NULL),
(35, 14, 7, 6, 1, 'ATTENTE', '2012-06-22 22:11:08', NULL),
(36, 0, 7, 6, 1, 'PREPAREE', '2012-06-22 22:53:02', NULL),
(37, 11, 7, 6, 1, 'PREPAREE', '2012-06-22 22:53:03', NULL),
(38, 0, 7, 6, 1, 'PREPAREE', '2012-06-22 22:56:54', NULL),
(39, 6, 7, 6, 1, 'PREPAREE', '2012-06-22 22:56:57', NULL),
(40, 0, 7, 6, 1, 'PREPAREE', '2012-06-22 22:59:38', NULL),
(41, 17.5, 7, 6, 1, 'PREPAREE', '2012-06-22 22:59:53', NULL),
(42, 0, 7, 6, 1, 'PREPAREE', '2012-06-22 23:01:48', NULL),
(43, 39.5, 7, 6, 1, 'PREPAREE', '2012-06-22 23:01:56', NULL),
(44, 0, 7, 6, 1, 'ATTENTE', '2012-06-22 23:07:29', NULL),
(45, 1.5, 7, 6, 1, 'ATTENTE', '2012-06-22 23:07:31', NULL),
(46, 0, 7, 6, 1, 'ATTENTE', '2012-06-22 23:33:22', NULL),
(47, 2, 7, 6, 1, 'ATTENTE', '2012-06-22 23:33:22', NULL),
(48, 0, 7, 6, 1, 'ATTENTE', '2012-06-22 23:35:58', NULL),
(49, 13, 7, 6, 1, 'ATTENTE', '2012-06-22 23:36:16', NULL),
(50, 0, 7, 6, 1, 'ATTENTE', '2012-06-22 23:49:53', NULL),
(51, 29.5, 7, 6, 1, 'ATTENTE', '2012-06-22 23:49:59', NULL),
(52, 0, 7, 6, 1, 'ATTENTE', '2012-06-22 23:54:14', NULL),
(54, 0, 7, 6, 1, 'ATTENTE', '2012-06-22 23:58:00', NULL),
(55, 10, 7, 6, 1, 'ATTENTE', '2012-06-22 23:58:06', NULL),
(56, 0, 7, 6, 1, 'ATTENTE', '2012-06-23 00:01:10', NULL),
(57, 39.5, 7, 6, 1, 'ATTENTE', '2012-06-23 00:01:13', NULL),
(58, 0, 7, 6, 1, 'ATTENTE', '2012-06-23 00:05:51', NULL),
(59, 29.5, 7, 6, 1, 'ATTENTE', '2012-06-23 00:05:53', NULL),
(61, 4.5, 7, 6, 1, 'ATTENTE', '2012-06-23 00:38:19', NULL),
(63, 22, 7, 6, 1, 'ATTENTE', '2012-06-23 00:46:35', NULL),
(64, 25, 7, 6, 1, 'ATTENTE', '2012-06-23 00:50:23', NULL),
(65, 14, 7, 6, 1, 'ATTENTE', '2012-06-23 00:54:54', NULL);

-- --------------------------------------------------------

--
-- Structure de la table `famille`
--

CREATE TABLE IF NOT EXISTS `famille` (
  `ID_FAMIL` int(11) NOT NULL AUTO_INCREMENT,
  `NOM` varchar(100) DEFAULT NULL,
  `IMAGE_URL` varchar(100) DEFAULT NULL,
  `DESCRIPTION` varchar(150) NOT NULL,
  PRIMARY KEY (`ID_FAMIL`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Contenu de la table `famille`
--

INSERT INTO `famille` (`ID_FAMIL`, `NOM`, `IMAGE_URL`, `DESCRIPTION`) VALUES
(1, 'boissons', 'pirates.png', 'sdsqdqsqdqsdqs'),
(2, 'pizzas', 'pizzas.png', ''),
(3, 'tex-mexs', 'tex-mexs.png', ''),
(4, 'menus', 'menus.png', ''),
(5, 'salades', 'salades.png', ''),
(6, 'paninis', 'paninis.png', '');

-- --------------------------------------------------------

--
-- Structure de la table `langue`
--

CREATE TABLE IF NOT EXISTS `langue` (
  `ID_LANG` int(11) NOT NULL AUTO_INCREMENT,
  `NOM` varchar(50) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `CODE` varchar(5) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`ID_LANG`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Contenu de la table `langue`
--

INSERT INTO `langue` (`ID_LANG`, `NOM`, `CODE`) VALUES
(1, 'Françaisss', 'FR'),
(2, 'English', 'EN'),
(3, 'Español', 'SP');

-- --------------------------------------------------------

--
-- Structure de la table `livreur`
--

CREATE TABLE IF NOT EXISTS `livreur` (
  `ID_LIVREUR` int(11) NOT NULL AUTO_INCREMENT,
  `NOM` varchar(50) NOT NULL,
  `PRENOM` varchar(50) NOT NULL,
  PRIMARY KEY (`ID_LIVREUR`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Contenu de la table `livreur`
--

INSERT INTO `livreur` (`ID_LIVREUR`, `NOM`, `PRENOM`) VALUES
(1, 'holaxxxxxxxxxxxxxxxxxxxx', 'prénom livreur 1 '),
(2, 'nom livreur 2 ', 'prénom nom livreur 2');

-- --------------------------------------------------------

--
-- Structure de la table `menu`
--

CREATE TABLE IF NOT EXISTS `menu` (
  `ID_MENU` int(11) NOT NULL AUTO_INCREMENT,
  `ID_PROD` int(11) NOT NULL,
  `NUM_COM` int(11) NOT NULL,
  PRIMARY KEY (`ID_MENU`),
  KEY `menu` (`ID_PROD`),
  KEY `commande_du_menu` (`NUM_COM`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=11 ;

--
-- Contenu de la table `menu`
--

INSERT INTO `menu` (`ID_MENU`, `ID_PROD`, `NUM_COM`) VALUES
(1, 9, 1),
(2, 10, 3),
(3, 10, 4),
(4, 9, 10),
(5, 9, 11),
(6, 9, 14),
(7, 9, 29),
(8, 9, 30),
(9, 10, 33),
(10, 10, 65);

-- --------------------------------------------------------

--
-- Structure de la table `produit`
--

CREATE TABLE IF NOT EXISTS `produit` (
  `ID_PROD` int(11) NOT NULL AUTO_INCREMENT,
  `NOM` varchar(50) DEFAULT NULL,
  `IMAGE_URL` varchar(100) DEFAULT NULL,
  `DESCRIPTION` varchar(200) DEFAULT NULL,
  `PRIX` float NOT NULL DEFAULT '0',
  `ID_FAMIL` int(11) NOT NULL,
  PRIMARY KEY (`ID_PROD`),
  KEY `famille_de_produit` (`ID_FAMIL`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=16 ;

--
-- Contenu de la table `produit`
--

INSERT INTO `produit` (`ID_PROD`, `NOM`, `IMAGE_URL`, `DESCRIPTION`, `PRIX`, `ID_FAMIL`) VALUES
(3, 'cocacola', 'algeria.jpg', 'zero 33cl', 1.5, 1),
(4, 'orangina', 'orangina.jpg', '33cl', 1.5, 1),
(5, 'oasis', 'oasis.png', '33 cl', 2, 1),
(6, 'pizza margarita', 'margarita.png', NULL, 10, 2),
(7, 'pizza 4 fromages', 'pizza_4_fromages.png', NULL, 12, 2),
(8, 'pizza végétarienne', 'vegetarienne.png', NULL, 13, 2),
(9, 'menu 1', 'zorro.jpg', 'menu1_description', 15, 4),
(10, 'menu 2', 'menu2.png', NULL, 14, 4),
(11, 'chiken 1', 'chiken1.png', NULL, 5, 3),
(12, 'chiken 2', 'chiken2.png', NULL, 6, 3),
(13, 'toto', NULL, 'Description', 4.5, 2),
(15, 'TestProd', 'URL', 'Desc1', 0, 6);

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
(1, 3, 1),
(1, 9, 1),
(2, 3, 1),
(2, 6, 1),
(3, 10, 1),
(4, 10, 2),
(7, 3, 0),
(8, 3, 0),
(9, 3, 0),
(9, 12, 0),
(10, 3, 0),
(10, 12, 0),
(11, 4, 0),
(14, 3, 0),
(14, 12, 0),
(16, 3, 0),
(29, 3, 0),
(29, 12, 0),
(30, 3, 0),
(30, 13, 0),
(33, 3, 0),
(33, 12, 0),
(35, 5, 0),
(35, 7, 0),
(37, 11, 0),
(37, 12, 0),
(39, 12, 0),
(39, 15, 0),
(41, 8, 0),
(41, 13, 0),
(43, 6, 0),
(43, 7, 0),
(43, 8, 0),
(43, 13, 0),
(45, 3, 0),
(47, 5, 0),
(49, 8, 0),
(51, 7, 0),
(51, 8, 0),
(51, 13, 0),
(51, 15, 0),
(55, 6, 0),
(57, 6, 0),
(57, 7, 0),
(57, 8, 0),
(57, 13, 0),
(59, 7, 0),
(59, 8, 0),
(59, 13, 0),
(61, 13, 0),
(63, 6, 0),
(63, 7, 0),
(64, 7, 0),
(64, 8, 0);

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
(1, 3, 1),
(2, 4, 1),
(3, 5, 1),
(5, 5, 0),
(9, 5, 0),
(10, 5, 0),
(1, 6, 1),
(2, 6, 1),
(2, 7, 1),
(3, 8, 2),
(5, 8, 0),
(9, 8, 0),
(10, 8, 0),
(4, 12, 0),
(6, 12, 0),
(7, 12, 0),
(8, 13, 0);

-- --------------------------------------------------------

--
-- Structure de la table `produit_composition`
--

CREATE TABLE IF NOT EXISTS `produit_composition` (
  `ID_FAMIL` int(11) NOT NULL,
  `ID_PROD` int(11) NOT NULL,
  `QUANTITE` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID_FAMIL`,`ID_PROD`),
  KEY `familles_produit` (`ID_FAMIL`),
  KEY `produits_famille` (`ID_PROD`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Contenu de la table `produit_composition`
--

INSERT INTO `produit_composition` (`ID_FAMIL`, `ID_PROD`, `QUANTITE`) VALUES
(1, 9, 1),
(1, 10, 1),
(2, 9, 1),
(2, 10, 2),
(3, 9, 1),
(5, 9, 5);

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