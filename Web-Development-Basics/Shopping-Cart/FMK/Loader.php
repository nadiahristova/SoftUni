<?php

namespace SoftUni\FMK;

final class Loader
{

    private static $namespaces = array();

    private function __construct()
    {

    }

    public static function registerAutoLoad()
    {
        spl_autoload_register(array('\SoftUni\FMK\Loader', 'autoload'));
    }

    public static function autoload($class)
    {
        self::loadClass($class);
    }

    public static function registerNamespace($namespace, $path)
    {
        $namespace = trim($namespace);
        if (strlen($namespace) > 0) {
            if (!$path) {
                throw new \Exception("Invalid path");
            }

            $_path = realpath($path);
            if ($_path && is_dir($_path) && is_readable($_path)) {
                self::$namespaces[$namespace . '\\'] = $_path . DIRECTORY_SEPARATOR;
            } else {
                //TODO
                throw new \Exception('Namespace directory read error: ' . $path);
            }
        } else {
            //TODO
            throw new \Exception("Invalid namespace");
        }
    }

    public static function registerNamespaces($namespacePaths)
    {
        if (is_array($namespacePaths)) {
            foreach ($namespacePaths as $namespace => $path) {
                self::registerNamespace($namespace, $path);
            }
        } else {
            throw new \Exception('Invalid namespaces Config.');
        }
    }

    public static function loadClass($class) {
        foreach (self::$namespaces as $namespace => $path) {
            if (strpos($class, $namespace) === 0) {
                $invariantSystemPath = str_replace('\\', DIRECTORY_SEPARATOR, $class);
                $filePath = substr_replace($invariantSystemPath, $path, 0, strlen($namespace)) . '.php';
                $realPath = realpath($filePath);
                if ($realPath && is_readable($realPath)) {
                    include $realPath;
                } else {
                    throw new \Exception('File cannot be included: ' . $filePath, 404);
                }
                break;
            }
        }
    }

    public static  function getNamespaces() {
        return self::$namespaces;
    }

    public static function removeNamespace($namespace) {
        unset(self::$namespaces[$namespace]);
    }

    public static function clearNamespaces() {
        self::$namespaces = array();
    }
}