-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Client: 127.0.0.1
-- Généré le : Sam 28 Janvier 2012 à 22:42
-- Version du serveur: 5.5.16
-- Version de PHP: 5.3.8

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données: `denapoli2`
--

-- --------------------------------------------------------

--
-- Structure de la table `commande`
--

CREATE TABLE IF NOT EXISTS `commande` (
  `NUM` int(11) NOT NULL AUTO_INCREMENT,
  `H_COMMANDE` datetime DEFAULT NULL,
  `H_TRAITEMENT` datetime DEFAULT NULL,
  `TOTAL` int(11) DEFAULT '0',
  `ID_ADR` int(11) DEFAULT NULL,
  `ID_CLIEN` int(11) DEFAULT NULL,
  `ID_BORN` int(11) DEFAULT NULL,
  `STATUT` varchar(20) NOT NULL,
  PRIMARY KEY (`NUM`),
  KEY `adresse_de_commande` (`ID_ADR`),
  KEY `client_qui_commande` (`ID_CLIEN`),
  KEY `borne_qui_commande` (`ID_BORN`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Contenu de la table `commande`
--

INSERT INTO `commande` (`NUM`, `H_COMMANDE`, `H_TRAITEMENT`, `TOTAL`, `ID_ADR`, `ID_CLIEN`, `ID_BORN`, `STATUT`) VALUES
(1, '2012-01-23 16:17:33', NULL, 0, 1, 1, 1, 'ATTENTE'),
(2, '2012-01-23 16:21:11', NULL, 0, 2, 2, 2, 'ATTENTE'),
(3, '2012-01-23 13:26:32', NULL, 0, 3, 3, 1, 'ATTENTE'),
(4, '2012-01-23 12:00:00', NULL, 0, 4, 2, 2, 'ATTENTE');

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `commande`
--
ALTER TABLE `commande`
  ADD CONSTRAINT `adresse_de_commande` FOREIGN KEY (`ID_ADR`) REFERENCES `adresse` (`ID_ADR`),
  ADD CONSTRAINT `borne_qui_commande` FOREIGN KEY (`ID_BORN`) REFERENCES `borne` (`ID_BORN`),
  ADD CONSTRAINT `client_qui_commande` FOREIGN KEY (`ID_CLIEN`) REFERENCES `client` (`ID_CLIEN`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
