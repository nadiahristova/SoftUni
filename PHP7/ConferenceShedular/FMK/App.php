<?php

namespace PFCS\FMK;

include_once 'Loader.php';

class App
{
    private static $_instance;
    private $_config = null;
    private $_frontController;
    private $router = null;
    private $_dbConnections;
    private $_session;

    private function __construct()
    {
        set_exception_handler(array($this, '_exceptionHandler'));

        Loader::registerNamespace('PFCS\FMK', dirname(__FILE__) . DIRECTORY_SEPARATOR);
        Loader::registerAutoLoad();
        $this->_config = Config::getInstance();
    }

    public function run()
    {
        if ($this->_config->getConfigFolder() == null) {
            $this->_config->setConfigFolder("../config");
        }

        $this->_frontController = \PFCS\FMK\FrontController::getInstance();
        if ($this->router instanceof \PFCS\FMK\Routers\IRouter) {
            $this->_frontController->setRouter($this->router);
        } else {
            $this->_frontController->setRouter(new \PFCS\FMK\Routers\DefaultRouter());
        }

//        $_sessionConfig = $this->_config->app['session'];
//        if ($_sessionConfig['autostart']) {
//            if ($_sessionConfig['type'] == 'native') {
//                $_s = new \PFCS\FMK\Session\NativeSession(
//                    $_sessionConfig['name'],
//                    $_sessionConfig['lifetime'],
//                    $_sessionConfig['path'],
//                    $_sessionConfig['domain'],
//                    $_sessionConfig['secure']);
//            } elseif ($_sessionConfig['type'] == 'database') {
//                $_s = new \PFCS\FMK\Session\DBSession(
//                    $_sessionConfig['dbConnection'],
//                    $_sessionConfig['name'],
//                    $_sessionConfig['dbTable'],
//                    $_sessionConfig['lifetime'],
//                    $_sessionConfig['path'],
//                    $_sessionConfig['domain'],
//                    $_sessionConfig['secure']
//                );
//            } else {
//                throw new \Exception('No valid session.', 500);
//            }
//
//            $this->setSession($_s);
//        }

        $this->_frontController->dispatch();
    }

    public static function getInstance() : App
    {
        if (self::$_instance == null) {
            self::$_instance = new App();
        }

        return self::$_instance;
    }

    public function setConfigFolder(string $path)
    {
        $this->_config->setConfigFolder($path);
    }

    public function getConfigFolder()
    {
        return $this->_config->getConfigFolder();
    }

    /**
     * @return Config|null
     */
    public function getConfig()
    {
        return $this->_config;
    }

    public function getRouter()
    {
        return $this->router;
    }

    public function setRouter($router)
    {
        $this->router = $router;
    }

//    public function setSession(\PFCS\FMK\Session\ISession $session)
//    {
//        $this->_session = $session;
//    }
//
//    public function getSession()
//    {
//        return $this->_session;
//    }

    public function getDBConnection($connection = 'default') : \PDO
    {
        if (!$connection) {
            throw new \Exception('No connection identifier provided.', 500);
        }
        if ($this->_dbConnections[$connection]) {
            return $this->_dbConnections[$connection];
        }

        $databaseConfig = $this->getConfig()->database;
        if (!$databaseConfig[$connection]) {
            throw new \Exception('No valid connection identifier provided.', 500);
        }

        $dbh = new \PDO(
            $databaseConfig[$connection]['dsn'],
            $databaseConfig[$connection]['username'],
            $databaseConfig[$connection]['password'],
            $databaseConfig[$connection]['pdo_options']);
        $this->_dbConnections[$connection] = $dbh;

        return $dbh;
    }

    public function displayError($errorCode)
    {
        try {
            $view = \PFCS\FMK\View::getInstance();
            View::title('Error ' . $errorCode);
            $view->appendToLayout('header', 'header');
            $view->appendToLayout('body', 'errors.' . $errorCode);
            $view->appendToLayout('footer', 'footer');
            $view->display('layouts.default');
        } catch(\Exception $ex) {
            $error = \PFCS\FMK\Common::headerStatus($errorCode);
            echo '<h1>' . $error . '</h1>';
            exit;
        }
    }

    public function _exceptionHandler(\Exception $ex)
    {
        if ($this->_config && $this->_config->app['displayExceptions'] === true) {
            echo '<pre>' . print_r($ex, true) . '</pre>';
        } else {
            $this->displayError($ex->getCode());
        }
    }

//    public function __destruct()
//    {
//        if ($this->_session != null) {
//            $this->_session->saveSession();
//        }
//    }

}