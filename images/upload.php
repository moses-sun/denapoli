<?php

$uploaddir = './'; // Relative Upload Location of data file

if (is_uploaded_file($_FILES['file']['tmp_name'])) {

$uploadfile = $uploaddir . basename($_FILES['file']['name']);

echo "File ". $_FILES['file']['name'] ." uploaded successfully. ";

if (move_uploaded_file($_FILES['file']['tmp_name'], $uploadfile)) {

echo "File is valid, and was successfully moved. ";

}

else

print_r($_FILES);

}

else {

echo "Upload Failed!!!";

print_r($_FILES);

}

?>