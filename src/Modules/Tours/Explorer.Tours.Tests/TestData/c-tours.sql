INSERT INTO tours."Tours"(
    "Id", "Name", "DifficultyLevel", "Description", "Tags", "Status", "Price", "GuideId", "PublishedDateTime", "ArchivedDateTime","TourCharacteristics")
VALUES (-1,'Tura 1',1,'opis',ARRAY['Abc', 'Def', 'Ghi'],0,2,-2,'2023-10-22 21:48:24.891+02','2023-10-22 21:48:24.891+02','[{"Distance": 1.029236386539564, "Duration": 9.332569538263399, "TransportType": 0},
      {"Distance": 4.029236386539564, "Duration": 7.332569538263399, "TransportType": 1}]');

INSERT INTO tours."Tours"(
    "Id", "Name", "DifficultyLevel", "Description", "Tags", "Status", "Price", "GuideId", "PublishedDateTime", "ArchivedDateTime","TourCharacteristics")
VALUES (-2,'Tura 2',1,'opis',ARRAY['Abc', 'Def', 'Ghi'], 0,2,-2,
    '2023-10-22 21:48:24.891+02','2023-10-22 21:48:24.891+02',
    '[{"Distance": 1.029236386539564, "Duration": 6.332569538263399, "TransportType": 0},
      {"Distance": 4.029236386539564, "Duration": 1.332569538263399, "TransportType": 1}]'
);

INSERT INTO tours."Tours"(
    "Id", "Name", "DifficultyLevel", "Description", "Tags", "Status", "Price", "GuideId", "PublishedDateTime","ArchivedDateTime", "TourCharacteristics")
VALUES (-3,'Tura 1', 1,'opis',
    ARRAY['Abc', 'Def', 'Ghi'],
    0,
    2,
    -2,
    '2023-10-22 21:48:24.891+02','2023-10-22 21:48:24.891+02',
    '[{"Distance": 1.029236386539564, "Duration": 1.332569538263399, "TransportType": 1},
      {"Distance": 4.029236386539564, "Duration": 5.332569538263399, "TransportType": 0}]'
);

INSERT INTO tours."Tours"(
    "Id", "Name", "DifficultyLevel", "Description", "Tags", "Status", "Price", "GuideId", "PublishedDateTime","ArchivedDateTime", "TourCharacteristics")
VALUES (
    -4,
    'Tura 3',
    1,
    'opis',
    ARRAY['Abc', 'Def', 'Ghi'],
    0,
    2,
    -2,
    '2023-10-22 21:48:24.891+02','2023-10-22 21:48:24.891+02',
    '[{"Distance": 1.029236386539564, "Duration": 2.332569538263399, "TransportType": 1},
      {"Distance": 4.029236386539564, "Duration": 5.332569538263399, "TransportType": 0}]'
);

INSERT INTO tours."Tours"(
    "Id", "Name", "DifficultyLevel", "Description", "Tags", "Status", "Price", "GuideId", "PublishedDateTime","ArchivedDateTime", "TourCharacteristics")
VALUES (
    -5,
    'Tura 4',
    1,
    'opis',
    ARRAY['Abc', 'Def', 'Ghi'],
    0,
    2,
    -2,
    '2023-10-22 21:48:24.891+02','2023-10-22 21:48:24.891+02',
    '[{"Distance": 1.029236386539564, "Duration": 8.332569538263399, "TransportType": 1},
      {"Distance": 4.029236386539564, "Duration": 2.332569538263399, "TransportType": 0}]'
);