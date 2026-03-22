CREATE DATABASE IF NOT EXISTS reactor_database;
USE reactor_database;

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

/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;

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
  UNIQUE KEY `id_cv_UNIQUE` (`id_cv`)
) ENGINE=InnoDB AUTO_INCREMENT=136 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

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
  UNIQUE KEY `id_rс_UNIQUE` (`id_rс`)
) ENGINE=InnoDB AUTO_INCREMENT=137 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `id_role` int NOT NULL AUTO_INCREMENT,
  `title_role` varchar(50) NOT NULL,
  PRIMARY KEY (`id_role`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

CREATE TABLE `temperature_charts` (
  `id_tc` int NOT NULL AUTO_INCREMENT,
  `temperature_heat_carrier_height_tc` double NOT NULL,
  `temperature_values_tc` varchar(100) NOT NULL,
  `temperature_values_tvel_tc` varchar(100) NOT NULL,
  PRIMARY KEY (`id_tc`),
  UNIQUE KEY `id_tc_UNIQUE` (`id_tc`)
) ENGINE=InnoDB AUTO_INCREMENT=136 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `research`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `research` (
  `id_research` int NOT NULL AUTO_INCREMENT,
  `title_research` varchar(100) NOT NULL,
  `success_research` varchar(10) NOT NULL,
  `user_id_research` int NOT NULL,
  `reactor_characteristics_id_research` int NOT NULL,
  `temperature_charts_id_research` int NOT NULL,
  `control_values_id_research` int NOT NULL,
  `date_research` varchar(50) NOT NULL,
  PRIMARY KEY (`id_research`),
  KEY `fk_research_reactor_characteristics1_idx` (`reactor_characteristics_id_research`),
  KEY `fk_research_temperature_charts1_idx` (`temperature_charts_id_research`),
  KEY `fk_research_control_values1_idx` (`control_values_id_research`),
  KEY `user_idx` (`user_id_research`),
  CONSTRAINT `fk_research_control_values1` FOREIGN KEY (`control_values_id_research`) REFERENCES `control_values` (`id_cv`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_research_reactor_characteristics1` FOREIGN KEY (`reactor_characteristics_id_research`) REFERENCES `reactor_characteristics` (`id_rс`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_research_temperature_charts1` FOREIGN KEY (`temperature_charts_id_research`) REFERENCES `temperature_charts` (`id_tc`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `user` FOREIGN KEY (`user_id_research`) REFERENCES `user` (`id_user`)
) ENGINE=InnoDB AUTO_INCREMENT=82 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id_user` int NOT NULL AUTO_INCREMENT,
  `role_id_user` int NOT NULL,
  `login_user` varchar(50) NOT NULL,
  `password_user` varchar(100) NOT NULL,
  `last_name_user` varchar(50) NOT NULL,
  `first_name_user` varchar(50) NOT NULL,
  `middle_name_user` varchar(50) DEFAULT NULL,
  `date_birth_user` date NOT NULL,
  `mail_user` varchar(50) NOT NULL,
  `research_permit_user` tinyint(1) NOT NULL,
  `image_user` varchar(100) NOT NULL,
  PRIMARY KEY (`id_user`),
  KEY `fk_user_role1_idx` (`role_id_user`),
  CONSTRAINT `fk_user_role1` FOREIGN KEY (`role_id_user`) REFERENCES `role` (`id_role`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

