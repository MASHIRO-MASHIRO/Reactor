-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: reactor_database
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `control_values`
--

DROP TABLE IF EXISTS `control_values`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `control_values` (
  `id_cv` int NOT NULL AUTO_INCREMENT,
  `reactor_core_volume_cv` double NOT NULL,
  `mode_cv` varchar(45) NOT NULL,
  `specific_volumetric_energy_cv` double NOT NULL,
  `velocity_coolant_core_cv` double NOT NULL,
  `average_heat_coolant_core_cv` double NOT NULL,
  `average_temperature_core_cv` double NOT NULL,
  `effective_height_cv` double NOT NULL,
  `effective_radius_cv` double NOT NULL,
  `coolant_consumption_cv` double NOT NULL,
  `specific_linear_center_cv` double NOT NULL,
  `heat_transfer_coefficient_outer_cv` double NOT NULL,
  `heat_transfer_coefficient_from_cv` double NOT NULL,
  `temperature_difference_cv` double NOT NULL,
  `temperature_outer_shell_cv` double NOT NULL,
  PRIMARY KEY (`id_cv`),
  UNIQUE KEY `id_cv_UNIQUE` (`id_cv`),
  CONSTRAINT `f_control_values` FOREIGN KEY (`id_cv`) REFERENCES `research` (`id_research`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `control_values`
--

LOCK TABLES `control_values` WRITE;
/*!40000 ALTER TABLE `control_values` DISABLE KEYS */;
INSERT INTO `control_values` VALUES (3,27.14,'Тарбулентный',109.43,5.19,5.61,305,3.758,1.664,0.413,33.89695,0.01356,33227,33.2,342.5),(6,27.14,'Тарбулентный',109.43,5.19,5.61,305,3.758,1.664,0.413,33.89695,0.01356,33227,33.2,342.5),(8,27.14,'Тарбулентный',109.43,830.16,5.61,305,3.758,1.664,66.058,5423.51268,0.17149,1159555,152.2,462),(16,27.14,'Тарбулентный',109.43,5.46,5.61,305,3.758,1.664,0.435,35.681,0.01391,34427,33.7,343),(19,27.14,'Тарбулентный',109.43,5.46,5.61,305,3.758,1.664,0.435,35.681,0.01391,34427,33.7,343),(20,27.14,'Тарбулентный',109.43,5.46,5.61,305,3.758,1.664,0.435,35.681,0.01391,34427,33.7,343),(22,27.14,'Тарбулентный',109.43,5.46,3.65,308,3.758,1.664,0.435,35.681,0.01391,28617,40.6,353.2),(23,27.14,'Тарбулентный',109.43,5.46,3.65,308,3.758,1.664,0.435,35.681,0.01391,28617,40.6,353.2),(24,27.14,'Тарбулентный',109.43,5.46,5.61,305,3.758,1.664,0.435,35.681,0.01391,34427,33.7,343),(25,27.14,'Тарбулентный',109.43,4.9,5.61,305,3.758,1.664,0.507,41.89214,0.01391,31572,43.2,352.4),(26,10.22,'Тарбулентный',269.08,46.15,3.74,300,3.132,1.141,0.307,23.40049,0.00936,172657,4.4,307.4),(27,10.22,'Тарбулентный',269.08,7014.94,3.74,300,3.132,1.141,46.69,3556.87382,0.11543,5813751,19.9,323.1),(28,27.14,'Тарбулентный',109.43,5.46,5.61,305,3.758,1.664,0.435,35.681,0.01391,34427,33.7,343);
/*!40000 ALTER TABLE `control_values` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reactor_characteristics`
--

DROP TABLE IF EXISTS `reactor_characteristics`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reactor_characteristics` (
  `id_rс` int NOT NULL AUTO_INCREMENT,
  `thermal_power_rc` int NOT NULL,
  `coolant_consumption_rc` int NOT NULL,
  `temperature_coolant_exit_rc` int NOT NULL,
  `temperature_coolant_entrance_rc` int NOT NULL,
  `coolant_pressure_rc` double NOT NULL,
  `coefficient_unevenness_heigth_rc` double NOT NULL,
  `coefficient_unevenness_radius_rc` double NOT NULL,
  `radius_rc` double NOT NULL,
  `heigth_rc` double NOT NULL,
  `effective_supplement_rc` double NOT NULL,
  `cassettes_in_active_zone_rc` int NOT NULL,
  `tvel_in_cassette_rc` int NOT NULL,
  `step_cassette_arrangement_rc` double NOT NULL,
  `cassette_wall_thickness_rc` double NOT NULL,
  PRIMARY KEY (`id_rс`),
  UNIQUE KEY `id_rс_UNIQUE` (`id_rс`),
  CONSTRAINT `f_reactor_characteristics` FOREIGN KEY (`id_rс`) REFERENCES `research` (`id_research`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reactor_characteristics`
--

LOCK TABLES `reactor_characteristics` WRITE;
/*!40000 ALTER TABLE `reactor_characteristics` DISABLE KEYS */;
INSERT INTO `reactor_characteristics` VALUES (3,2970,15960,289,321,16,1.5,1.37,1.56,3.55,0.104,160,317,0.241,0.0015),(6,2970,15960,289,321,16,1.5,1.37,1.56,3.55,0.104,160,317,0.241,0.0015),(8,2970,15960,289,321,16,1.5,1.37,1.56,3.55,0.104,1,317,0.241,0.0015),(16,2970,15960,289,321,16,1.5,1.37,1.56,3.55,0.104,152,317,0.241,0.0015),(19,2970,15960,289,321,16,1.5,1.37,1.56,3.55,0.104,152,317,0.241,0.0015),(20,2970,15960,289,321,16,1.5,1.37,1.56,3.55,0.104,152,317,0.241,0.0015),(22,2970,15960,289,327,10.5,1.5,1.37,1.56,3.55,0.104,152,317,0.241,0.0015),(23,2970,15960,289,327,10.5,1.5,1.37,1.56,3.55,0.104,152,317,0.241,0.0015),(24,2970,15960,289,321,16,1.5,1.37,1.56,3.55,0.104,152,317,0.241,0.0015),(25,2970,15960,289,321,16,1.5,1.37,1.56,3.55,0.104,152,270,0.241,0.0015),(26,2750,13000,300,300,10.5,1.01,1.02,1.05,2.95,0.091,152,270,0.172,0.0001),(27,2750,13000,300,300,10.5,1.01,1.02,1.05,2.95,0.091,1,270,0.172,0.0001),(28,2970,15960,289,321,16,1.5,1.37,1.56,3.55,0.104,152,317,0.241,0.0015);
/*!40000 ALTER TABLE `reactor_characteristics` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `research`
--

DROP TABLE IF EXISTS `research`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `research` (
  `id_research` int NOT NULL AUTO_INCREMENT,
  `title_research` varchar(50) NOT NULL,
  `success_research` varchar(10) NOT NULL,
  `user_id_research` int NOT NULL,
  `reactor_characteristics_id_research` int NOT NULL,
  `temperature_charts_id_research` int NOT NULL,
  `control_values_id_research` int NOT NULL,
  `date_research` varchar(50) NOT NULL,
  PRIMARY KEY (`id_research`),
  UNIQUE KEY `id_research_UNIQUE` (`id_research`),
  KEY `user_idx` (`user_id_research`),
  CONSTRAINT `user` FOREIGN KEY (`user_id_research`) REFERENCES `user` (`id_user`)
) ENGINE=InnoDB AUTO_INCREMENT=90 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `research`
--

LOCK TABLES `research` WRITE;
/*!40000 ALTER TABLE `research` DISABLE KEYS */;
INSERT INTO `research` VALUES (3,'Исследование 3 (user45)','Успешное',45,3,3,3,'25.02.2026 16:38:28'),(6,'Исследование 4 (user34)','Успешное',34,6,6,6,'01.03.2026 12:42:54'),(8,'Исследование 6 (user34)','Провальное',34,8,8,8,'08.03.2026 13:23:57'),(16,'Исследование 14 (user35)','Успешное',35,16,16,16,'12.03.2026 21:06:27'),(19,'Исследование 17 (user0)','Успешное',62,19,19,19,'15.03.2026 13:05:57'),(20,'Исследование 18 (tir)','Успешное',63,20,20,20,'15.03.2026 13:34:16'),(22,'Исследование 11 (user35)','Провальное',35,22,22,22,'17.03.2026 13:06:35'),(23,'Исследование 12 (user35)','Провальное',35,23,23,23,'17.03.2026 13:06:39'),(24,'Исследование 13 (user35)','Успешное',35,24,24,24,'17.03.2026 16:36:13'),(25,'Исследование 14 (user35)','Провальное',35,25,25,25,'17.03.2026 16:36:23'),(26,'Исследование 15 (user35)','Успешное',35,26,26,26,'17.03.2026 16:36:50'),(27,'Исследование 15 (user35)','Успешное',35,27,27,27,'18.03.2026 19:33:09'),(28,'Исследование 13 (user35)','Успешное',35,28,28,28,'19.03.2026 22:37:01');
/*!40000 ALTER TABLE `research` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `id_role` int NOT NULL AUTO_INCREMENT,
  `title_role` varchar(50) NOT NULL,
  PRIMARY KEY (`id_role`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Сотрудник лаборатории'),(2,'Администратор');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `temperature_charts`
--

DROP TABLE IF EXISTS `temperature_charts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `temperature_charts` (
  `id_tc` int NOT NULL AUTO_INCREMENT,
  `temperature_heat_carrier_height_tc` double NOT NULL,
  `temperature_values_tc` varchar(100) NOT NULL,
  `temperature_values_tvel_tc` varchar(100) NOT NULL,
  PRIMARY KEY (`id_tc`),
  UNIQUE KEY `id_tc_UNIQUE` (`id_tc`),
  CONSTRAINT `f_temperature_charts` FOREIGN KEY (`id_tc`) REFERENCES `research` (`id_research`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `temperature_charts`
--

LOCK TABLES `temperature_charts` WRITE;
/*!40000 ALTER TABLE `temperature_charts` DISABLE KEYS */;
INSERT INTO `temperature_charts` VALUES (3,3.55,'289 294.4 305.7 317 322.4','291.9 318.9 338.9 341.5 325.3'),(6,3.55,'289 294.4 305.7 317 322.4','291.9 318.9 338.9 341.5 325.3'),(8,3.55,'289 294.4 305.7 317 322.4','302.2 406.6 457.9 429.2 335.6'),(16,3.55,'289 294.4 305.7 317 322.4','291.9 319.2 339.4 341.8 325.3'),(19,3.55,'289 294.4 305.7 317 322.4','291.9 319.2 339.4 341.8 325.3'),(20,3.55,'289 294.4 305.7 317 322.4','291.9 319.2 339.4 341.8 325.3'),(22,3.55,'289 297.3 314.7 332.1 340.3','292.5 327.2 355.3 362 343.8'),(23,3.55,'289 297.3 314.7 332.1 340.3','292.5 327.2 355.3 362 343.8'),(24,3.55,'289 294.4 305.7 317 322.4','291.9 319.2 339.4 341.8 325.3'),(25,3.55,'289 294.4 305.7 317 322.4','292.8 326.2 348.9 348.8 326.2'),(26,2.95,'300 306.2 319.2 332.2 338.5','300.4 309.4 323.6 335.4 338.9'),(27,2.95,'300 306.2 319.2 332.2 338.5','301.8 320.9 339.1 346.9 340.3'),(28,3.55,'289 294.4 305.7 317 322.4','291.9 319.2 339.4 341.8 325.3');
/*!40000 ALTER TABLE `temperature_charts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id_user` int NOT NULL AUTO_INCREMENT,
  `role_id_user` int NOT NULL,
  `login_user` varchar(20) NOT NULL,
  `password_user` varchar(100) NOT NULL,
  `last_name_user` varchar(50) NOT NULL,
  `first_name_user` varchar(50) NOT NULL,
  `middle_name_user` varchar(50) DEFAULT NULL,
  `date_birth_user` date NOT NULL,
  `mail_user` varchar(20) NOT NULL,
  `research_permit_user` tinyint(1) NOT NULL,
  `image_user` varchar(100) NOT NULL,
  PRIMARY KEY (`id_user`),
  KEY `fk_user_role1_idx` (`role_id_user`),
  CONSTRAINT `fk_user_role1` FOREIGN KEY (`role_id_user`) REFERENCES `role` (`id_role`)
) ENGINE=InnoDB AUTO_INCREMENT=69 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (2,1,'user2','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Прохорова','Валентина','Ивановна','2026-02-02','user2@mail.ru',0,'default_image1.png'),(3,1,'user3','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Пуговкин','Евгений','Сергеевич','1990-12-10','user3@mail.ru',1,'default_image2.png'),(4,1,'user4','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Башнев','Николай','Николаевич','1998-10-20','user4@mail.ru',1,'default_image3.png'),(5,1,'user5','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Городов','Александр','Александрович','1990-12-10','user5@mail.ru',1,'default_image2.png'),(6,1,'user6','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Прохоров','Кирилл','Александрович','1991-11-03','user6@mail.ru',1,'default_image3.png'),(7,1,'user7','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Коткин','Николай','Кириллович','1991-11-03','user7@mail.ru',1,'default_image2.png'),(8,1,'user8','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Порохоров','Иван','Николаевич','1990-12-10','user8@mail.ru',1,'default_image4.png'),(9,1,'user9','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Грознов','Сергей','Сергеевич','1998-10-20','user9@mail.ru',1,'default_image3.png'),(10,1,'user10','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Выдров','Александр','Сергеевич','1990-12-10','user10@mail.ru',1,'default_image4.png'),(11,1,'user11','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Радилова','Наталья','Александровна','1998-10-12','user11@mail.ru',1,'default_image3.png'),(12,1,'user12','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Конева','Евгения','Евгеньевна','1998-10-12','user12@mail.ru',0,'default_image4.png'),(13,1,'user13','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Лисова','Александра','Евгеньевна','1990-08-11','user13@mail.ru',1,'default_image5.png'),(14,1,'user14','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Евгеньевна','Наталья','Ивановна','1998-10-12','user14@mail.ru',1,'default_image2.png'),(15,1,'user15','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Бокова','Екатерина','Александровна','1998-10-12','user15@mail.ru',1,'default_image5.png'),(16,1,'user16','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Иванова','Ольга','Ивановна','1998-10-12','user16@mail.ru',1,'default_image2.png'),(17,1,'user17','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Радилова','Василиса','Николаевна','1998-10-12','user17@mail.ru',1,'default_image5.png'),(18,1,'user18','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Речнова','Надежда','Григорьевна','1990-08-11','user18@mail.ru',1,'default_image4.png'),(19,1,'user19','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Серафимова','Евгения','Сергеевна','1998-10-12','user19@mail.ru',1,'default_image4.png'),(20,1,'user20','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Ликерова','Виолетта','Тимофеевна','1990-08-11','user20@mail.ru',0,'default_image2.png'),(21,1,'user21','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Оленева','Александра','Николаевна','1998-10-12','user21@mail.ru',1,'default_image5.png'),(22,1,'user22','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Яблокова','Любовь','Александровна','1990-08-11','user22@mail.ru',0,'default_image2.png'),(23,1,'user23','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Мурома','Надежда','Ивановна','1998-10-12','user23@mail.ru',1,'default_image3.png'),(24,1,'user24','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Чичикова','Любовь','Саламонова','1990-08-11','user24@mail.ru',1,'default_image5.png'),(25,1,'user25','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Земляничкина','Наталья','Евгеньевна','1998-10-12','user25@mail.ru',0,'default_image3.png'),(26,1,'user26','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Прохорова','Виолетта','Ивановна','1998-10-12','user26@mail.ru',0,'default_image5.png'),(27,1,'user27','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Лугова','Ольга','Александровна','1998-10-12','user27@mail.ru',1,'default_image3.png'),(28,1,'user28','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Персикова','Любовь','Сергеевна','1990-08-11','user28@mail.ru',1,'default_image4.png'),(29,1,'user29','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Радилова','Екатерина','Тимофеевна','1998-10-12','user29@mail.ru',1,'default_image2.png'),(30,1,'user30','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Башнева','Александра','Евгеньевна','1998-10-12','user30@mail.ru',1,'default_image2.png'),(31,1,'user31','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Кирова','Василиса','Саламонова','1990-08-11','user31@mail.ru',1,'default_image2.png'),(32,1,'user32','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Радиловна','Любовь','Григорьевна','1998-10-12','user32@mail.ru',1,'default_image3.png'),(33,1,'user33','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Земева','Екатерина','Николаевна','1998-10-12','user33@mail.ru',0,'default_image5.png'),(34,1,'user34','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Речнова','Виолетта','Тимофеевна','1998-10-12','user34@mail.ru',1,'default_image3.png'),(35,1,'user35','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Змеева','Любовь','Николаевна','1990-08-11','user35@mail.ru',1,'default_image2.png'),(36,1,'user36','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Кроловна','Наталья','Сергеевна','1990-08-11','user36@mail.ru',0,'default_image2.pngdefault_image2.png'),(37,1,'user37','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Городова','Надежда','Александровна','1998-10-12','user37@mail.ru',1,'default_image2.png'),(38,1,'user38','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Грушева','Любовь','Ивановна','1998-10-12','user38@mail.ru',0,'default_image2.png'),(39,1,'user39','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Радилова','Евгения','Саламонова','1990-08-11','user39@mail.ru',1,'default_image3.png'),(40,1,'user40','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Березова','Ольга','Сергеевна','1998-10-12','user40@mail.ru',1,'default_image3.png'),(41,1,'user41','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Травина','Василиса','Григорьевна','1990-08-11','user41@mail.ru',1,'default_image5.png'),(42,1,'user42','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Осинова','Виолетта','Евгеньевна','1990-08-11','user42@mail.ru',1,'default_image5.png'),(43,1,'user43','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Ясенева','Ольга','Ивановна','1990-08-11','user43@mail.ru',1,'default_image4.png'),(44,1,'user44','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Гривина','Александра','Николаевна','1998-10-12','user44@mail.ru',0,'default_image3.png'),(45,1,'user45','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Конева','Василиса','Евгеньевна','2008-02-29','user85@mail.ru',1,'default_image2.png'),(47,1,'user47','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Волкова','Екатерина','Сергеевна','2008-03-10','user479@mail.ru',1,'default_image2.png'),(54,2,'kot','zQXCKD9it8dJEQCN9qZhAdUe1csj5rS1yEr0vGDbDzo=','Щербаков','Антон','Юрьевич','2003-02-13','kot@mail.ru',0,'default_image4.png'),(55,1,'kotofei','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Котова','Анастасия','Владимировна','2025-12-09','kotofei@mail.ru',1,'default_image5.png'),(58,2,'Andronova','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Андронова','София','Андреевна','2000-03-15','Andronova@mail.ru',0,'default_image3.png'),(59,2,'user159','CAXc3ELKR6vcPY/hH44MehCGAgIvcas0lkjP3TCnWqY=','Аглаева','София','Александровна','2004-05-19','user349@mail.ru',0,'Снимок экрана 2025-05-27 182255.png'),(61,2,'kot2','nFb0ium6/SBSYgNL/MIjKyxjNIy3I9aB7DnxNAn5kMw=','Брежнев','Анатолий','Степанович','2008-03-10','weferfr@mail.ru',0,'Диаграмма без названия.drawio.png'),(62,1,'user0','nFb0ium6/SBSYgNL/MIjKyxjNIy3I9aB7DnxNAn5kMw=','Вадима','Алиса','Владимировна','2008-02-05','user6667@mail.ru',1,'Диаграмма без названия.drawio.png'),(63,1,'tir','nFb0ium6/SBSYgNL/MIjKyxjNIy3I9aB7DnxNAn5kMw=','Брежнев','Анатолий','Андреевич','1979-01-02','tir@mail.ru',1,'Снимок экрана 2025-03-16 173046.png'),(66,1,'user67','nFb0ium6/SBSYgNL/MIjKyxjNIy3I9aB7DnxNAn5kMw=','Василиеса','Анна','Сергеевна','1981-02-11','user67@mail.ru',0,'Снимок экрана 2025-03-16 173046.png'),(67,1,'zwef23t','g0pwm6JTTr4+4Tl/1Pe9KIsqzB0goI1shi3NmbbwRAA=','Цуацу','Ацупцу','Уцацупа','1999-02-03','wef34@mail.ru',0,'user_image.png'),(68,1,'wefwe43','nFb0ium6/SBSYgNL/MIjKyxjNIy3I9aB7DnxNAn5kMw=','Ыацу','Цупцупа','Цйцак','1999-02-03','f221e@mail.ru',0,'user_image.png');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-03-22 17:43:04
