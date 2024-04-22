-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Apr 22, 2024 at 04:00 PM
-- Server version: 5.7.24
-- PHP Version: 7.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `talpa`
--

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `id` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `team` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`id`, `name`, `email`, `team`) VALUES
('auth0|6605320b6ea7bff45012a6a1', 'rafael', '532430@student.fontys.nl', 0),
('auth0|6605320b6ea7bff45012a6a11', 'Medewerker 2', '2@student.fontys.nl', 0),
('auth0|6605320b6ea7bff45012a6a2', 'Medewerker 3', '3@student.fontys.nl', 0),
('auth0|6605320b6ea7bff45012a6a3', 'Medewerker 4', '4@student.fontys.nl', 0),
('auth0|6605320b6ea7bff45012a6a4', 'Medewerker 5', '5@student.fontys.nl', 0),
('auth0|6605320b6ea7bff45012a6a5', 'Medewerker 6', '6@student.fontys.nl', 0),
('auth0|6605320b6ea7bff45012a6a6', 'Medewerker 7', '7@student.fontys.nl', 0),
('auth0|6605320b6ea7bff45012a6a7', 'Medewerker 8', '8@student.fontys.nl', 0),
('auth0|6605320b6ea7bff45012a6a8', 'Medewerker 1', '1@student.fontys.nl', 0),
('auth0|6605320b6ea7bff45012a6a9', 'Medewerker 1', '1@student.fontys.nl', 0);

-- --------------------------------------------------------

--
-- Table structure for table `suggestions`
--

CREATE TABLE `suggestions` (
  `id` int(11) NOT NULL,
  `user` varchar(255) NOT NULL,
  `suggestion` varchar(500) NOT NULL,
  `description` varchar(1000) NOT NULL,
  `time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `suggestions`
--

INSERT INTO `suggestions` (`id`, `user`, `suggestion`, `description`, `time`) VALUES
(18, 'auth0|6605320b6ea7bff45012a6a1', 'Bowlen', 'Geniet van een gezellige en competitieve activiteit met collega\'s terwijl je probeert zoveel mogelijk kegels om te gooien.', '2024-04-22 14:28:11'),
(19, 'auth0|6605320b6ea7bff45012a6a1', 'Zwemmen', 'Ontspan en verfris jezelf met een verkwikkende zwemsessie in een zwembad of recreatiegebied.', '2024-04-22 14:28:12'),
(20, 'auth0|6605320b6ea7bff45012a6a1', 'Lazergamen', 'Ga de strijd aan met je collega\'s in een spannend spel van verborgen strategie en tactiek.', '2024-04-22 14:28:12'),
(21, 'auth0|6605320b6ea7bff45012a6a1', 'Kookworkshop', 'Ontdek je culinaire talenten en werk samen aan het bereiden van heerlijke gerechten onder begeleiding van een professionele chef-kok.', '2024-04-22 14:28:12'),
(22, 'auth0|6605320b6ea7bff45012a6a1', 'Wandeltocht', 'Verken de natuurlijke schoonheid van een nabijgelegen park of bos tijdens een ontspannen wandeltocht met je collega\'s.', '2024-04-22 14:28:12'),
(23, 'auth0|6605320b6ea7bff45012a6a1', 'Escape Room', 'Werk samen om puzzels op te lossen en raadsels te ontrafelen om te ontsnappen uit een uitdagende en meeslepende escaperoom.', '2024-04-22 14:30:00'),
(24, 'auth0|6605320b6ea7bff45012a6a1', 'Barbecueën', 'Geniet van een informele en gezellige barbecue in de buitenlucht, compleet met heerlijk gegrild eten en verfrissende drankjes.', '2024-04-22 14:30:00'),
(25, 'auth0|6605320b6ea7bff45012a6a1', 'Rondvaart', 'Ontspan en geniet van het uitzicht tijdens een ontspannen rondvaart langs de grachten of waterwegen van de stad.', '2024-04-22 14:30:00'),
(26, 'auth0|6605320b6ea7bff45012a6a1', 'Paintball', 'Ervaar de spanning van een intense en opwindende paintballwedstrijd terwijl je samenwerkt met je teamgenoten om de overwinning te behalen.', '2024-04-22 14:30:00'),
(27, 'auth0|6605320b6ea7bff45012a6a1', 'Filmavond', 'Organiseer een gezellige filmavond waarbij je samen met je collega\'s geniet van een selectie van films en snacks.', '2024-04-22 14:31:28'),
(28, 'auth0|6605320b6ea7bff45012a6a1', 'Stand-up comedy show', 'Lach de avond weg tijdens een hilarische stand-up comedy show met optredens van getalenteerde comedians.', '2024-04-22 14:31:28'),
(29, 'auth0|6605320b6ea7bff45012a6a1', 'Paint & Sip', 'Laat je creativiteit de vrije loop terwijl je onder begeleiding van een kunstenaar een meesterwerk schildert, terwijl je geniet van een lekker drankje.', '2024-04-22 14:31:28'),
(30, 'auth0|6605320b6ea7bff45012a6a1', 'Bowling', 'Geniet van een gezellige en competitieve activiteit met collega\'s terwijl je probeert zoveel mogelijk kegels om te gooien.', '2024-04-22 14:31:28'),
(31, 'auth0|6605320b6ea7bff45012a6a1', 'Picknick in het park', 'Geniet van een ontspannen dag in de buitenlucht met een heerlijke picknick in een nabijgelegen park, compleet met dekens en lekkernijen.', '2024-04-22 14:31:28'),
(32, 'auth0|6605320b6ea7bff45012a6a1', 'Stadswandeling', 'Verken de bezienswaardigheden en verborgen juweeltjes van de stad tijdens een ontspannen wandeling met je collega\'s.', '2024-04-22 14:32:18'),
(33, 'auth0|6605320b6ea7bff45012a6a1', 'Karten', 'Voel de adrenaline stromen terwijl je racet tegen je collega\'s op een spannend indoor kartcircuit.', '2024-04-22 14:32:18'),
(34, 'auth0|6605320b6ea7bff45012a6a1', 'Trampolinepark', 'Spring en stuiter op trampolines en geniet van een energieke en leuke activiteit met je collega\'s in een trampolinepark.', '2024-04-22 14:32:18'),
(35, 'auth0|6605320b6ea7bff45012a6a1', 'Yoga in het park', 'Ontspan en kom tot rust tijdens een verkwikkende yogasessie in de frisse lucht van het park, geleid door een ervaren yogaleraar.', '2024-04-22 14:32:18'),
(36, 'auth0|6605320b6ea7bff45012a6a1', 'Klimmen in het klimpark', 'Durf de hoogte op te zoeken en uitdagende klimroutes te trotseren in een avontuurlijk klimpark, geschikt voor zowel beginners als ervaren klimmers.', '2024-04-22 14:32:18'),
(37, 'auth0|6605320b6ea7bff45012a6a1', 'Boerengolf', 'Ervaar een unieke variant van golf in de buitenlucht, waarbij je met een klomp aan een stok de bal richting de hole slaat op een schilderachtig boerenlandschap.', '2024-04-22 14:32:18');

-- --------------------------------------------------------

--
-- Table structure for table `suggestion_categorie`
--

CREATE TABLE `suggestion_categorie` (
  `suggestion_id` int(11) NOT NULL,
  `categorie` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `suggestion_categorie`
--

INSERT INTO `suggestion_categorie` (`suggestion_id`, `categorie`, `description`) VALUES
(11, 'Buiten', '-'),
(11, 'Water', '-'),
(11, 'Middag', '-'),
(12, 'Eten', '-'),
(12, 'Water', '-'),
(12, 'Avond', '-'),
(17, 'Binnen', '-'),
(17, 'Avond', '-'),
(17, 'Binnen', '-'),
(17, 'Water', '-'),
(17, 'Binnen', '-'),
(17, 'Avond', '-'),
(18, 'Binnen', '-'),
(18, 'Avond', '-'),
(19, 'Binnen', '-'),
(19, 'Buiten', '-'),
(19, 'Water', '-'),
(19, 'Middag', '-'),
(20, 'Binnen', '-'),
(20, 'Avond', '-'),
(21, 'Binnen', '-'),
(21, 'Eten', '-'),
(21, 'Middag', '-'),
(22, 'Buiten', '-'),
(22, 'Ochtend', '-'),
(22, 'Middag', '-'),
(22, 'Groepsgrootte', '-'),
(23, 'Binnen', '-'),
(23, 'Avond', '-'),
(24, 'Buiten', '-'),
(24, 'Eten', '-'),
(24, 'Avond', '-'),
(25, 'Buiten', '-'),
(25, 'Water', '-'),
(25, 'Avond', '-'),
(26, 'Buiten', '-'),
(26, 'Middag', '-'),
(26, 'Groepsgrootte', '-'),
(27, 'Binnen', '-'),
(27, 'Avond', '-'),
(28, 'Binnen', '-'),
(28, 'Avond', '-'),
(29, 'Binnen', '-'),
(29, 'Avond', '-'),
(30, 'Binnen', '-'),
(30, 'Avond', '-'),
(31, 'Buiten', '-'),
(31, 'Middag', '-'),
(32, 'Buiten', '-'),
(32, 'Middag', '-'),
(33, 'Binnen', '-'),
(33, 'Middag', '-'),
(33, 'Groepsgrootte', '-'),
(34, 'Binnen', '-'),
(34, 'Middag', '-'),
(34, 'Groepsgrootte', '-'),
(35, 'Buiten', '-'),
(35, 'Ochtend', '-'),
(36, 'Buiten', '-'),
(36, 'Middag', '-'),
(36, 'Groepsgrootte', '-'),
(37, 'Buiten', '-'),
(37, 'Middag', '-'),
(37, 'Groepsgrootte', '-');

-- --------------------------------------------------------

--
-- Table structure for table `suggestion_limitation`
--

CREATE TABLE `suggestion_limitation` (
  `suggestion_id` int(11) NOT NULL,
  `limitation` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `suggestion_limitation`
--

INSERT INTO `suggestion_limitation` (`suggestion_id`, `limitation`, `description`) VALUES
(7, 'Mobility', '-'),
(7, 'Intellect', '-'),
(7, 'Age', '-'),
(8, 'Mobility', '-'),
(8, 'GroupSize', '-'),
(9, 'Time', '-'),
(10, 'Intellect', '-'),
(10, 'Height', '-'),
(11, 'Tijd', '-'),
(11, 'Dieet', '-'),
(12, 'Leeftijd', '-'),
(12, 'Lengte', '-'),
(12, 'Allergie', '-'),
(17, 'Mobiliteit', '-'),
(17, 'Tijd', '-'),
(18, 'Mobiliteit', '-'),
(18, 'Tijd', '-'),
(19, 'Tijd', '-'),
(19, 'Leeftijd', '-'),
(19, 'Dieet', '-'),
(19, 'Allergie', '-'),
(20, 'Mobiliteit', '-'),
(20, 'Tijd', '-'),
(21, 'Dieet', '-'),
(21, 'Allergie', '-'),
(22, 'Mobiliteit', '-'),
(22, 'Leeftijd', '-'),
(23, 'Mobiliteit', '-'),
(23, 'Tijd', '-'),
(24, 'Dieet', '-'),
(24, 'Allergie', '-'),
(25, 'Mobiliteit', '-'),
(25, 'Leeftijd', '-'),
(26, 'Mobiliteit', '-'),
(26, 'Leeftijd', '-'),
(33, 'Mobiliteit', '-'),
(34, 'Mobiliteit', '-'),
(34, 'Leeftijd', '-'),
(36, 'Mobiliteit', '-'),
(36, 'Leeftijd', '-');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `suggestions`
--
ALTER TABLE `suggestions`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `suggestions`
--
ALTER TABLE `suggestions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
