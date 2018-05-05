SET IDENTITY_INSERT SolutionTypes ON 
INSERT SolutionTypes (Id, Name) values (1, 'Product'), (2, 'Project'), (3, 'Service');
SET IDENTITY_INSERT SolutionTypes OFF

SET IDENTITY_INSERT Currencies ON 
INSERT Currencies (Id, Name) values (1, 'USD'), (2, 'EUR'), (3, 'AUD'), (4, 'JPY'), (5, 'GBP'), (6, 'RMB');
SET IDENTITY_INSERT Currencies OFF

SET IDENTITY_INSERT ImplementationStatus ON 
INSERT ImplementationStatus (Id, Name) values (1, 'Idea'), (2, 'Prototype'), (3, 'Startup'), (4, 'Extension');
SET IDENTITY_INSERT ImplementationStatus OFF 

SET IDENTITY_INSERT Industries ON 
INSERT Industries (Id, Name) values (1, 'Architecture'), (2, 'Security'), (3, 'Bioresources'), (4, 'Biotechnology'), (5, 'Vending'), (6, 'Hotel business'), (7, 'Woodworking'),
(8, 'Design'), (9, 'Home and office'), (10, 'Animals / goods for animals'), (11, 'Publishing house / Polygraphy'), (12, 'Investment / Finance'), (13, 'Industrial parks'), (14, 'Engineering'),
(15, 'Information Technology'), (16, 'Research'), (17, 'Consulting'), (18, 'Lottery activities'), (19, 'Marketing'), (20, 'Mechanical engineering'), (21, 'Medicine / Pharmaceuticals'),
(22, 'Metalworking'), (23, 'Fashion and beauty'), (24, 'Music / art / show business'), (25, 'The science'), (26, 'The property'), (27, 'Refining / Petrochemicals'), (28, 'Know-How'),
(29, 'Education'), (30, 'Catering'), (31, 'Recreation'), (32, 'Transport / Transportation / Logistics'), (33, 'Recycling'), (34, 'Processing of polymers'), (35, 'Projects for children'),
(36, 'Industry'), (37, 'Mining industry'), (38, 'Industry light'), (39, 'Food industry'), (40, 'Chemical industry'), (41, 'Entertainment / TV / Media'), (42, 'Crop production'),
(43, 'Advertising'), (44, 'Robotics'), (45, 'Communications'), (46, 'Agriculture'), (47, 'Building'), (48, 'Trade'), (49, 'Tourism'),
(50, 'The services'), (51, 'Power Engineering');
SET IDENTITY_INSERT Industries OFF 