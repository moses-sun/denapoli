-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Client: 127.0.0.1
-- Généré le : Ven 09 Mars 2012 à 21:42
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
  PRIMARY KEY (`ID_ADR`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=8 ;

--
-- Contenu de la table `adresse`
--

INSERT INTO `adresse` (`ID_ADR`, `NUM`, `VOIE`, `COMPLEMENT`, `CP`, `VILLE`) VALUES
(1, 26, 'Rue Bouquet', 'Logement 126', '77185', 'LOGNES'),
(2, 1, 'Rue jean jaures', NULL, '75006', 'Paris'),
(3, 5, 'boulevard aussman', NULL, '75017', 'Paris'),
(4, 34, 'Rue toto', NULL, '75001', 'Paris'),
(5, 7, 'rue magique', NULL, '77420', 'Champs-Sur-Marne'),
(6, 8, 'rue toto', NULL, '78001', 'Montesson'),
(7, 9, 'rue de paris', NULL, '93720', 'Montreuil');

-- --------------------------------------------------------

--
-- Structure de la table `borne`
--

CREATE TABLE IF NOT EXISTS `borne` (
  `ID_BORN` int(11) NOT NULL AUTO_INCREMENT,
  `ID_ADR` int(11) NOT NULL,
  PRIMARY KEY (`ID_BORN`),
  KEY `adresse_de_borne` (`ID_ADR`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Contenu de la table `borne`
--

INSERT INTO `borne` (`ID_BORN`, `ID_ADR`) VALUES
(2, 6),
(1, 7);

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Contenu de la table `client`
--

INSERT INTO `client` (`ID_CLIEN`, `NOM`, `PRENOM`, `TEL`, `EMAIL`) VALUES
(1, 'chandarli', 'younes', '0664783697', 'younes@yahoo.fr'),
(2, 'mohi', 'toto', '0765345678', 'toto@gmail.fr'),
(3, 'rachid', 'rachid', '0654678965', 'rachid.gmail.com'),
(4, 'Masson', 'Damien', '0654678955', 'hola@jojo.fr');

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
  PRIMARY KEY (`NUM`),
  KEY `adresse_de_commande` (`ID_ADR`),
  KEY `client_qui_commande` (`ID_CLIEN`),
  KEY `borne_qui_commande` (`ID_BORN`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Contenu de la table `commande`
--

INSERT INTO `commande` (`NUM`, `TOTAL`, `ID_ADR`, `ID_CLIEN`, `ID_BORN`, `STATUT`) VALUES
(1, 0, 1, 1, 1, 'ATTENTE'),
(2, 0, 2, 2, 2, 'ATTENTE'),
(3, 0, 3, 3, 1, 'ATTENTE'),
(4, 0, 4, 2, 2, 'ATTENTE');

-- --------------------------------------------------------

--
-- Structure de la table `famille`
--

CREATE TABLE IF NOT EXISTS `famille` (
  `ID_FAMIL` int(11) NOT NULL AUTO_INCREMENT,
  `NOM` varchar(100) DEFAULT NULL,
  `IMAGE_URL` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID_FAMIL`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Contenu de la table `famille`
--

INSERT INTO `famille` (`ID_FAMIL`, `NOM`, `IMAGE_URL`) VALUES
(1, 'boissons', 'boissons.png'),
(2, 'pizzas', 'pizzas.png'),
(3, 'tex-mexs', 'tex-mexs.png'),
(4, 'menus', 'menus.png'),
(5, 'salades', 'salades.png'),
(6, 'paninis', 'paninis.png');

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Contenu de la table `menu`
--

INSERT INTO `menu` (`ID_MENU`, `ID_PROD`, `NUM_COM`) VALUES
(1, 9, 1),
(2, 10, 3),
(3, 10, 4);

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=13 ;

--
-- Contenu de la table `produit`
--

INSERT INTO `produit` (`ID_PROD`, `NOM`, `IMAGE_URL`, `DESCRIPTION`, `PRIX`, `ID_FAMIL`) VALUES
(3, 'cocacola', 'cocacola.png', 'zero 33cl', 1.5, 1),
(4, 'orangina', 'orangina.jpg', '33cl', 1.5, 1),
(5, 'oasis', 'oasis.png', '33 cl', 2, 1),
(6, 'pizza margarita', 'margarita.png', NULL, 10, 2),
(7, 'pizza 4 fromages', 'pizza_4_fromages.png', NULL, 12, 2),
(8, 'pizza végétarienne', 'vegetarienne.png', NULL, 13, 2),
(9, 'menu 1', 'menu1.png', NULL, 15, 4),
(10, 'menu 2', 'menu2.png', NULL, 14, 4),
(11, 'chiken 1', 'chiken1.png', NULL, 5, 3),
(12, 'chiken 2', 'chiken2.png', NULL, 6, 3);

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
(4, 10, 2);

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
(1, 6, 1),
(2, 6, 1),
(2, 7, 1),
(3, 8, 2);

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
(2, 10, 2);

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
  ADD CONSTRAINT `client_qui_commande` FOREIGN KEY (`ID_CLIEN`) REFERENCES `client` (`ID_CLIEN`);

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
