<?php
	include './lib/debug.php';
	if ($argc>1) parse_str ($argv[1],$_REQUEST);
?>
<html debug=true>
	<head>
		<title>Main</title>
		<link rel="stylesheet" type="text/css" href="app/assets/styles/test.css">
	</head>
	<body>
		<h1>It works if you read it and it is blue</h1>
		<img src="app/assets/images/icon.png">
		<form action="http://localhost/index.php" method="get">
			<input name="x" value="1">
			<input name="y" value="2">
			<input type="submit">
		</form>

	<?php
		debug ($_REQUEST, '$_REQUEST');
	?>

	</body>
</html>