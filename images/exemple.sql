CREATE TABLE `article` (
  `ART_ID` int(11) NOT NULL DEFAULT '0',
  `ART_LIBELLE` varchar(20) DEFAULT NULL,
  `ART_PRIXBASEHT` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`ART_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

 
insert into`article`(`ART_ID`,`ART_LIBELLE`,`ART_PRIXBASEHT`) values (1,'IPOD',149);
insert into`article`(`ART_ID`,`ART_LIBELLE`,`ART_PRIXBASEHT`) values (2,'XBOX360',234);
insert into`article`(`ART_ID`,`ART_LIBELLE`,`ART_PRIXBASEHT`) values (3,'PSP3',399);
insert into`article`(`ART_ID`,`ART_LIBELLE`,`ART_PRIXBASEHT`) values (4,'PSP',150);
insert into`article`(`ART_ID`,`ART_LIBELLE`,`ART_PRIXBASEHT`) values (5,'WII',200);


CREATE TABLE `tiers` (
  `TRS_ID` int(11) NOT NULL DEFAULT '0',
  `TRS_NOM` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`TRS_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

 
insert into `tiers`(`TRS_ID`,`TRS_NOM`) values (1,'JASKULA');
insert into `tiers`(`TRS_ID`,`TRS_NOM`) values (2,'NODEVO');
insert into `tiers`(`TRS_ID`,`TRS_NOM`) values (3,'DUPONT');


CREATE TABLE `commande` (
  `CDE_ID` int(11) NOT NULL DEFAULT '0',
  `CDE_NUM` varchar(10) DEFAULT NULL,
  `CDE_DATE` date DEFAULT NULL,
  `TRS_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`CDE_ID`),
  KEY `TRS_ID` (`TRS_ID`),
  CONSTRAINT `commande_ibfk_1` FOREIGN KEY (`TRS_ID`) REFERENCES `tiers` (`TRS_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

 
insert into `commande`(`CDE_ID`,`CDE_NUM`,`CDE_DATE`,`TRS_ID`) values (1,'CDE 1','2009-01-02 00:00:00',1);
insert into `commande`(`CDE_ID`,`CDE_NUM`,`CDE_DATE`,`TRS_ID`) values (2,'CDE 2','2009-01-08 00:00:00',1);
insert into `commande`(`CDE_ID`,`CDE_NUM`,`CDE_DATE`,`TRS_ID`) values (3,'CDE 3','2009-01-13 00:00:00',1);
insert into `commande`(`CDE_ID`,`CDE_NUM`,`CDE_DATE`,`TRS_ID`) values (4,'CDE 4','2009-01-06 00:00:00',2);
insert into `commande`(`CDE_ID`,`CDE_NUM`,`CDE_DATE`,`TRS_ID`) values (5,'CDE 5','2009-01-21 00:00:00',2);
insert into `commande`(`CDE_ID`,`CDE_NUM`,`CDE_DATE`,`TRS_ID`) values (6,'CDE 6','2009-02-04 00:00:00',3);



CREATE TABLE `lignes` (
  `LGN_ID` int(11) NOT NULL DEFAULT '0',
  `LGN_QTE` int(11) DEFAULT NULL,
  `LGN_TOTALHT` decimal(10,0) DEFAULT NULL,
  `CDE_ID` int(11) DEFAULT NULL,
  `ART_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`LGN_ID`),
  KEY `CDE` (`CDE_ID`),
  KEY `ART` (`ART_ID`),
  CONSTRAINT `ART` FOREIGN KEY (`ART_ID`) REFERENCES `article` (`ART_ID`),
  CONSTRAINT `CDE` FOREIGN KEY (`CDE_ID`) REFERENCES `commande` (`CDE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

insert into `lignes`(`LGN_ID`,`LGN_QTE`,`LGN_TOTALHT`,`CDE_ID`,`ART_ID`) values (1,2,468,1,2);
insert into `lignes`(`LGN_ID`,`LGN_QTE`,`LGN_TOTALHT`,`CDE_ID`,`ART_ID`) values (2,10,3990,1,3);
insert into `lignes`(`LGN_ID`,`LGN_QTE`,`LGN_TOTALHT`,`CDE_ID`,`ART_ID`) values (3,5,745,2,1);
insert into `lignes`(`LGN_ID`,`LGN_QTE`,`LGN_TOTALHT`,`CDE_ID`,`ART_ID`) values (4,2,798,3,3);
insert into `lignes`(`LGN_ID`,`LGN_QTE`,`LGN_TOTALHT`,`CDE_ID`,`ART_ID`) values (5,15,2250,3,4);
insert into `lignes`(`LGN_ID`,`LGN_QTE`,`LGN_TOTALHT`,`CDE_ID`,`ART_ID`) values (6,1,200,3,5);
insert into `lignes`(`LGN_ID`,`LGN_QTE`,`LGN_TOTALHT`,`CDE_ID`,`ART_ID`) values (7,1,200,4,5);
insert into `lignes`(`LGN_ID`,`LGN_QTE`,`LGN_TOTALHT`,`CDE_ID`,`ART_ID`) values (8,10,2340,5,2);
insert into `lignes`(`LGN_ID`,`LGN_QTE`,`LGN_TOTALHT`,`CDE_ID`,`ART_ID`) values (9,6,894,6,1);
insert into `lignes`(`LGN_ID`,`LGN_QTE`,`LGN_TOTALHT`,`CDE_ID`,`ART_ID`) values (10,10,1500,6,5);