<?php

namespace SoftUni\FMK\DB;


class SimpleDB {
    private $_db = null;
    protected $_connection = 'default';
    private static $database = null;
    private $_statement = null;
    private $_params = array();
    private $_sql;

    public function __construct($connection = null)
    {
        if ($connection instanceof \PDO) {
            $this->_db = $connection;
            //self::$database = $connection;
        } else if ($connection != null) {
            $this->_db = \SoftUni\FMK\App::getInstance()->getDBConnection($connection);
            //self::$database = \SoftUni\FMK\App::getInstance()->getDBConnection($connection);
            $this->_connection = $connection;
        } else {
            $this->_db = \SoftUni\FMK\App::getInstance()->getDbConnection($this->_connection);
            //self::$database = \SoftUni\FMK\App::getInstance()->getDbConnection($this->_connection);
        }
    }

    public function prepare($sql, $params = array(), $pdoOptions = array())
    {
        $this->_statement = $this->_db->prepare($sql, $pdoOptions);
        $this->_params = $params;
        $this->_sql = $sql;
        return $this;
    }

    public function execute($params = array())
    {
        if ($params) {
            $this->_params = $params;
        }
        $this->_statement->execute($this->_params);
        return $this;
    }
    public function fetchAllAssoc($escape = true)
    {
        $data = $this->_statement->fetchAll(\PDO::FETCH_ASSOC);
        if ($data === false) {
            return false;
        }

        if ($escape) {
            $escaped = array();
            foreach ($data as $elementKey => $elementData) {
                foreach ($elementData as $key => $value) {
                    $escaped[$elementKey][$key] = htmlentities($value);
                }

            }

            return $escaped;
        }

        return $data;
    }

    public function fetchRowAssoc($escape = true)
    {
        $data = $this->_statement->fetch(\PDO::FETCH_ASSOC);
        if ($data === false) {
            return false;
        }

        if ($escape) {
            $escaped = array();
            foreach ($data as $key => $value) {
                $escaped[$key] = htmlentities($value);
            }

            return $escaped;
        }

        return $data;
    }

    public function getLastInsertedId()
    {
        return $this->_db->lastInsertId();
    }

}