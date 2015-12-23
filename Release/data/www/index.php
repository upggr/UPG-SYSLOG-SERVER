<!DOCTYPE html>
<html lang="en">
<head>

</head>
<body>

<?php
error_reporting(E_ALL);
$logloaded = $_GET["logfile"];
function selectedvalue($value1,$value2) 
{	
if ($value1 == $value2) { return 'selected="selected"';}
}
?>

<form action="index.php" method="get"><input type=button value="Refresh this Page if things get funny" onClick="window.location.reload()"></form>
<form>
Select the log you want to process
<select name="logfile" id="logfile">
<?php
foreach (glob("*.txt") as $filename) {
	echo '<option value="'.$filename.'" '.selectedvalue($filename,$logloaded).'>'.$filename.'</option>';
}
?>
</select>
<input name="Go" type="submit" id="Go" formaction="index.php" formmethod="GET" value="Go">
</form>

<?php
if (isset($logloaded))
{
	//echo $logloaded;
	$lines = file($logloaded);
	foreach ($lines as $line) {
		$pieces = explode(" ", $line);
			foreach ($pieces as $piece) {
			echo $piece;
			echo '-----';
			}
			//echo $line.'<br>';
			echo '<br>';
			echo '<br>';
		}
	
}
else  {
	echo 'ffffff';
}

?>
</body>
</html>