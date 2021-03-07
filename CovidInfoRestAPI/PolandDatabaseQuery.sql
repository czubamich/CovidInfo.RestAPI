DELETE FROM Countries
INSERT INTO Countries
VALUES ('PL','Poland','Polska')

DELETE FROM Regions
INSERT INTO Regions
VALUES 
	('DS','PL','dolnośląskie'),
	('KP','PL','kujawsko-pomorskie'),
	('LB','PL','lubelskie'),
	('LD','PL','lubuskie'),
	('LU','PL','łódzkie'),
	('MA','PL','małopolskie'),
	('MZ','PL','mazowieckie'),
	('OP','PL','opolskie'),
	('PD','PL','podkarpackie'),
	('PK','PL','podlaskie'),
	('PM','PL','pomorskie'),
	('SK','PL','śląskie'),
	('SL','PL','świętokrzyskie'),
	('WN','PL','warmińsko-mazurskie'),
	('WP','PL','wielkopolskie'),
	('ZP','PL','zachodniopomorskie')

DELETE FROM CountriesHistory
INSERT INTO CountriesHistory
VALUES 
	('11-24-2020','PL','11-24-2020 11:00:00 AM',250000,20000,100000,2000,5000,300,250000,40000,250000)

DELETE FROM RegionsHistory
INSERT INTO RegionsHistory
VALUES 
	('11-24-2020','MZ','11-24-2020 11:00:00 AM',25000,2000,10000,200,500,30,25000,3000,25000),
	('11-24-2020','SK','11-24-2020 11:00:00 AM',35000,1000,10000,100,400,30,28000,1500,15000),
	('11-24-2020','DS','11-24-2020 11:00:00 AM',15000,1000,10000,100,300,30,22000,3000,17000)