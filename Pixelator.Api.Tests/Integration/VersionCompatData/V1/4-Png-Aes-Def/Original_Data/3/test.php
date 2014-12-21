<?php

namespace Test;

$resource = \imagecreate(1, 1);
var_dump((string)$resource, gettype($resource));
$resource;
var_dump((string)$resource, gettype($resource));
die();
$t = 'testst';
var_dump($t[2], $t['sdggds'], $t{2}, $t->{2});
die();

$t = null;
var_dump($t);
die();

define('_>_', 'hello');
die(_>_);

$PDO = new \PDO('mysql:host=localhost;dbname=penumbratest', 'root', 'admin');
$Statement = $PDO->prepare('SELECT * FROM penumbratest.blogs LIMIT 100');
$Statement->execute();
var_dump($Statement->fetchAll(\PDO::FETCH_GROUP | \PDO::FETCH_ASSOC, 'test'));

die();

function ftok($pathname, $proj_id) {
   $st = @stat($pathname);
   if (!$st) {
       return -1;
   }
  
   $key = sprintf("%u", (($st['ino'] & 0xffff) | (($st['dev'] & 0xff) << 16) | (($proj_id & 0xff) << 24)));
   return $key;
}

$shm_key = ftok(__FILE__, 'p');
$shm_id = shm_attach($shm_key, "c", 0644, 100);
$count = shmop_read($shm_id, 0, 1);
$count = is_string($count) ? 0 : $count;
var_dump($count);
$count++;
shmop_write($shm_id, (string)$count, 0);

\shmop_close($shm_id);
die();

var_dump(\PDO::getAvailableDrivers());
die();

$T = new \SplFileObject('dsggdsdsgs');
die();

$Reflection = new \ReflectionFunction('strlen');
var_dump($Reflection->getParameters());
die();

var_dump(call_user_func_array('isset', [true]));

class Func {
	public function __toString() {
		var_dump(__METHOD__);
		return 'rand';
	}
}
$Func = new Func();
$Func();
die();

class Test {

}
define('Test\\Test::test', 'CONSTANT');
var_dump(Test::test);
die();

$Arrayiterator = new \RecursiveArrayIterator([1,4, [4,5,4,[4,3,65]]]);
$Iterator = new \RecursiveIteratorIterator($Arrayiterator);
var_dump(iterator_to_array($Iterator));
die();

        
var_dump(range(2, 10, 2));
die();

var_dump(__NAMESPACE__ . '\\');
die();


date_default_timezone_set(date_default_timezone_get());
var_dump(serialize(new \DateTime()));
die();

$LongArray = array_fill_keys(range(0, 20000), array('E' => 'FooBar'));

$start = microtime(true);
$MapResetArray = array_map('reset', $LongArray);
$resettime = microtime(true) - $start;

$start = microtime(true);
$MapFuncArray = array_map(function($Value) { return $Value['E']; }, $LongArray);
$functiontime = microtime(true) - $start;

$start = microtime(true);
$MapForeachArray = [];
foreach ($LongArray as $key => $value) {
    $MapForeachArray[$key] = $value['E'];
}
$forachtime = microtime(true) - $start;

var_dump('reset', $resettime, 'function', $functiontime, 'foreach', $forachtime);
die();


final class Binary {
    //Arthmetic
    const Addition = '+';
    const Subtraction = '-';
    const Multiplication = '*';
    const Division = '/';
    const Modulus = '%';
    
    //Bitwise
    const BitwiseAnd = '&';
    const BitwiseOr = '|';
    const BitwiseXor = '^';
    const ShiftLeft = '<<';
    const ShiftRight = '>>';
    
    //Logical
    const LogicalAnd = '&&';
    const LogicalOr = '||';
    const Equality = '==';
    const Identity = '===';
    const Inequality = '!=';
    const NotIdentitical = '!==';
    const LessThan = '<';
    const LessThanOrEqualTo = '<=';
    const GreaterThan = '>';
    const GreaterThanOrEqualTo = '>=';
    
    //String
    const Concatenation = '.';
    
    //Type
    const IsInstanceOf = 'instanceof';
}
$BinaryOperations = [
            Binary::BitwiseAnd =>             function ($L, $R) { return $L & $R; },
            Binary::BitwiseOr =>              function ($L, $R) { return $L | $R; },
            Binary::BitwiseXor =>             function ($L, $R) { return $L ^ $R; },
            Binary::ShiftLeft =>              function ($L, $R) { return $L << $R; },
            Binary::ShiftRight =>             function ($L, $R) { return $L >> $R; },
            Binary::LogicalAnd =>             function ($L, $R) { return $L && $R; },
            Binary::LogicalOr =>              function ($L, $R) { return $L || $R; },
            Binary::Addition =>               function ($L, $R) { return $L + $R; },
            Binary::Subtraction =>            function ($L, $R) { return $L - $R; },
            Binary::Multiplication =>         function ($L, $R) { return $L * $R; },
            Binary::Division =>               function ($L, $R) { return $L / $R; },
            Binary::Modulus =>                function ($L, $R) { return $L % $R; },
            Binary::Concatenation =>          function ($L, $R) { return $L . $R; },
            Binary::IsInstanceOf =>           function ($L, $R) { return $L instanceof $R; },
            Binary::Equality =>               function ($L, $R) { return $L == $R; },
            Binary::Identity =>               function ($L, $R) { return $L === $R; },
            Binary::Inequality =>             function ($L, $R) { return $L != $R; },
            Binary::NotIdentitical =>         function ($L, $R) { return $L !== $R; },
            Binary::LessThan =>               function ($L, $R) { return $L < $R; },
            Binary::LessThanOrEqualTo =>      function ($L, $R) { return $L <= $R; },
            Binary::GreaterThan =>            function ($L, $R) { return $L > $R; },
            Binary::GreaterThanOrEqualTo =>   function ($L, $R) { return $L >= $R; },
        ];
function BinaryOperation($Left, $Operator, $Right) {
    $Operation = $GLOBALS['BinaryOperations'][$Operator];
    return $Operation($Left, $Right);
}

$L = 'sfdfds';
$R = 34234;
$Operator = Binary::Concatenation;

$F = $E = '';
$start = microtime(true);
for ($index = 0; $index < 10000; $index++) {
    eval('$E .= $L ' . $Operator .' $R;');
}
$evaltime = microtime(true) - $start;

$start = microtime(true);
for ($index = 0; $index < 10000; $index++) {
    $F .= BinaryOperation($L, $Operator, $R);
}
$functiontime = microtime(true) - $start;
var_dump($E === $F);
var_dump('eval', $evaltime, 'function', $functiontime);
die();

function FOO(&$Ref) { return $Ref = 'bar'; }

var_dump(FOO($gsgdsg));
var_dump(FOO($dsfdsf));
var_dump(FOO($gsgdsg));
var_dump(FOO($sdfdfds));
die();

$Array = [];
$Array[null] = 'test';
$Array[null] = 'two';
var_dump($Array);
die();


$PDO = new \PDO('mysql:host=localhost;dbname=test', 'root', 'admin');
$Statement = $PDO->prepare('SELECT * FROM stormtest.blogs LIMIT 2');
$Statement->execute();

var_dump($Statement->fetchAll(\PDO::FETCH_ASSOC));
die();

function test () { return ';'; }
/* @var $Reflection \ReflectionFunctionAbstract */
$Reflection = new \ReflectionFunction('Test\test');
var_dump($Reflection->getShortName());
die();


$Values = [1,2,3, 'test' => 'foo', 4.53];
$ArrayObject = new \ArrayObject($Values);
var_dump('Original array:');
var_dump($Values);
var_dump('ArrayObject cast to array:');
$CastArrayObject = (array)$ArrayObject;
var_dump($CastArrayObject);
var_dump('Are identical:');
var_dump($CastArrayObject === $Values);

die();

var_dump(get_defined_functions());
die();

$Test;
function &Reference($Value) {
	$GLOBALS['__STORM_REFERENCES__'] =& $Value;
	return $Value;
}
class Foo {
	public $Bar;
	public function __construct() {
		$T = [1,2,3,4];
		$this->setBar($T);
	}
	public function setBar(&$Val) {
		$this->Bar =& $Val;
	}
}
$Array = [1,2,3];
$Foo = new Foo($Array);
$Clone = clone $Foo;

$Foo->Bar[] = 'Test';
var_dump($Clone);
die();


var_dump(__NAMESPACE__);
die();

$test = ['test' => $t = 'bar'];
$test2 = ['test' => null];

$testi = array_intersect_key($test, $test2);
$test['test'] = 'foo';
var_dump($testi);
die();

$test = ['test' => 'bar', 45 => true, 'hello' => '', 1 => 2, 3 => 4];
var_dump(array_map(function ($val) { return $val . 'test'; }, $test));
die();

class Foo {
	private $Var;
	public function __construct(&$Var) {
		$this->Var =& $Var;
	}
}

$Ref = 'Hello';
$Foo = new Foo($Ref);
$FooTwo = clone $Foo;
$Ref = 'Changed';

var_dump($Foo, $FooTwo);
die();


$Foo = 'Test';
$Test = array (1 => &$Foo);
$Intesect = array_intersect_key($Test, array(1 => true));
$Intesect[1] = 'Bar';
var_dump($Foo);

die();

abstract class Foo {
    
    public static function Make() {
        return new static();
    }
}

var_dump(Foo::Make());
die();

echo true;
echo false;
die();

class FooBar implements ArrayAccess {
    
    public function __get($name) {
        var_dump($name);
    }
    public function offsetExists($offset) {
        
    }

    public function offsetGet($offset) {
        var_dump($offset);
    }

    final public function offsetSet($offset, $value) {
        'esg';
    }

    public function offsetUnset($offset) {
        
    }

}
$Foo = new FooBar();
$Foo->{['hi', 'prop']};//Non
$Foo[['hi', 'indexor']];//Oui

die();


$EvaledClosure = eval('return function () { return \'Foo\'; };');
$Reflection = new ReflectionFunction($EvaledClosure);
var_dump($Reflection->getFileName());
var_dump($Reflection->getStartLine());
var_dump($Reflection->getEndLine());
die();

'test';
657;
657.54;
<<<Test

Test;
die();

class Foo {
	public static $Bar = null;
}
$Name = 'Bar';
Foo::$$Name = function () {
	return 'Hi';
};
var_dump(Foo::$Bar());

die();
$PDO = new PDO('mysql:host=localhost;dbname=test', 'root', 'admin');
$Statement = $PDO->prepare('INSERT INTO `test`.`child` (`Id`, `ParentId`, `ChildData`) VALUES (DEFAULT, 2, \'Edited\')');
$Statement->execute();
var_dump($PDO->lastInsertId('parent'));

die();
class NotHere {
	private $Hello = 'Hi!';
	private $ThisReference;
	public function __construct() {
	
	}
	public function SetReference(&$ThisReference) {
		$this->ThisReference =& $ThisReference;
	}
	
	private function Destroy() {
		$this->ThisReference = null;
	}
	
	public function __get($Name) {
		$this->Destroy();
		return $this->$Name;
	}
	
	public function __set($Name, $Value) {
		$this->Destroy();
		return $this->$Name = $Value;
	}
}

$Test = new stdClass();
$Test->Foo = new NotHere();
$Test->Foo->SetReference($Test->Foo);
var_dump($Test->Foo->T);
var_dump($Test->Foo);

die();

class Foo {}

$Foo = new Foo();
$Foo->Bar = 'Hi';
$Foo->Baz = null;

var_dump(class_parents($Foo, false));
var_dump(property_exists($Foo, 'Baz'));

?>