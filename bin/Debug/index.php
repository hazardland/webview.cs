<?php
	include './lib/debug.php';
	if ($argc>1) parse_str ($argv[1],$_REQUEST);
?>
<head>
	<title>Main</title>
</head>
<body>
	<h1>It works</h1>
	<form action="http://localhost/index.php" method="get">
		<input name="x" value="1">
		<input name="y" value="2">
		<input type="submit">
	</form>

<?php
	debug ($_REQUEST, '$_REQUEST');
?>

</body>