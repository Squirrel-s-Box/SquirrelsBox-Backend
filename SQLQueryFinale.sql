CREATE DATABASE db_squirrels_box_2

CREATE TABLE sessions (
    id INT IDENTITY PRIMARY KEY,
    code VARCHAR(40) NOT NULL UNIQUE,
    attempt INT DEFAULT 0,
    creation_date DATETIME DEFAULT GETDATE(),
    last_update_date DATETIME NULL
);

CREATE TABLE users_sessions (
    id INT IDENTITY PRIMARY KEY,
    token NVARCHAR(255),
    old_token NVARCHAR(255) NULL,
    creation_date DATETIME DEFAULT GETDATE() NOT NULL,
    last_update_date DATETIME NULL,
    user_id int NOT NULL,  -- Assuming user_code in users_sessions references code in users
    FOREIGN KEY (user_id) REFERENCES sessions (id)
);

CREATE TABLE devices_sessions (
    id INT IDENTITY PRIMARY KEY,
    token NVARCHAR(255),
    creation_date DATETIME DEFAULT GETDATE() NOT NULL,
    last_update_date DATETIME NULL,
    user_id int NOT NULL,  -- Assuming user_code in users_sessions references code in users
    FOREIGN KEY (user_id) REFERENCES sessions (id)
);

DROP TABLE users_data
CREATE TABLE users_data (
    id INT IDENTITY PRIMARY KEY,
	username NVARCHAR(40),
    email VARCHAR(80) NOT NULL,
    name VARCHAR(80) NOT NULL,
    lastname VARCHAR(80) NOT NULL,
    user_photo VARCHAR(MAX) NULL,
    user_code VARCHAR(40) NOT NULL,
    creation_date DATETIME2 DEFAULT GETUTCDATE() NOT NULL,
    last_update_date DATETIME2 NULL,
    state BIT DEFAULT 1
);

CREATE TABLE boxes (
    id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(60),
    --box_type INT, -- 0(Private) - 1(Private) - 2(Only Some)
    --downloadable BIT DEFAULT 0,
	user_code_owner VARCHAR(40) NOT NULL,
    favourite BIT DEFAULT 0,
    creation_date DATETIME DEFAULT GETDATE(),
    last_update_date DATETIME NULL,
    state BIT DEFAULT 1
);

CREATE TABLE shared_boxes (
    id INT IDENTITY PRIMARY KEY,
    --box_role INT, -- 0(Viewer)	- 1(Editor)		- 2(Commenter)
	box_id INT NOT NULL,
	user_code_guest VARCHAR(40) NOT NULL,
    creation_date DATETIME DEFAULT GETDATE(),
    last_update_date DATETIME NULL,
    state BIT NOT NULL,
    FOREIGN KEY (box_id) REFERENCES boxes (id)
);

CREATE TABLE sections (
    id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(60),
    creation_date DATETIME DEFAULT GETDATE(),
    last_update_date DATETIME NULL,
    state BIT NOT NULL
);

CREATE TABLE boxes_sections_list (
    box_id INT NOT NULL,
    section_id INT NOT NULL,
    PRIMARY KEY (box_id, section_id),
    FOREIGN KEY (box_id) REFERENCES boxes (id),
    FOREIGN KEY (section_id) REFERENCES sections (id)
);

CREATE TABLE items (
    id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(60),
	description NVARCHAR(60) NULL,
	amount NVARCHAR(60) NULL,
	item_photo NVARCHAR(60) NULL,
    creation_date DATETIME DEFAULT GETDATE(),
    last_update_date DATETIME NULL,
    state BIT NOT NULL
);

CREATE TABLE sections_items_list (
    section_id INT NOT NULL,
    item_id INT NOT NULL,
    PRIMARY KEY (section_id, item_id),
    FOREIGN KEY (section_id) REFERENCES sections (id),
    FOREIGN KEY (item_id) REFERENCES items (id)
);

CREATE TABLE personalized_specs (
    id INT IDENTITY PRIMARY KEY,
    header_name NVARCHAR(60),
	value NVARCHAR(60) NULL,
	value_type NVARCHAR(60) NULL,
    creation_date DATETIME DEFAULT GETDATE(),
    last_update_date DATETIME NULL,
    state BIT NOT NULL
);

CREATE TABLE personalized_specs_items_list (
    item_id INT NOT NULL,
    presonalized_spec_id INT NOT NULL,
    PRIMARY KEY (item_id, presonalized_spec_id),
    FOREIGN KEY (item_id) REFERENCES items (id),
    FOREIGN KEY (presonalized_spec_id) REFERENCES personalized_specs (id)
);

CREATE TABLE permissions(
	id INT IDENTITY PRIMARY KEY,
	collection NVARCHAR(60),
    name NVARCHAR(60),
	value NVARCHAR(60)
);

CREATE TABLE boxes_permissions (
    id INT IDENTITY PRIMARY KEY,
	box_id INT NOT NULL,
    permission_id INT NOT NULL,
	user_code VARCHAR(40) NOT NULL,
    FOREIGN KEY (permission_id) REFERENCES permissions (id)
);

CREATE TABLE shared_boxes_permissions (
	id INT IDENTITY PRIMARY KEY,
    shared_box_id INT NOT NULL,
    permission_id INT NOT NULL,
    FOREIGN KEY (permission_id) REFERENCES permissions (id)
);