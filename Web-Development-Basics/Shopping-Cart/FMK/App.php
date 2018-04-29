<?php

namespace SoftUni\FMK;

use SoftUni\FMK\Routers\DefaultRouter;
use SoftUni\Sessions\ISession;
use SoftUni\Sessions\NativeSession;

include_once 'Loader.php';

class App{
    private static $_instance;
    private $_config = null;
    private $_frontController;
    private $_router = null;
    private $_dbConnections = [];
    /** @var /SoftUni/FMK/Sessions/ISession */
    private $_session = null;

    public function getRouter()
    {
        return $this->_router;
    }

    public function setRouter($router)
    {
        $this->_router = $router;
    }


    private function __construct() {
        set_exception_handler(array($this, '_exceptionHandler'));

        \SoftUni\FMK\Loader::registerNamespace('SoftUni\FMK', dirname(__FILE__).DIRECTORY_SEPARATOR);
        \SoftUni\FMK\Loader::registerAutoLoad();
        $this->_config = \SoftUni\FMK\Config::getInstance();
        if($this->_config->getConfigFolder() == null){
            $this->setConfigFolder('../Config');
        }
    }

    public function run(){
        //if config folder is not set, use defaults
        if($this->_config->getConfigFolder() == null){
            $this->_config->setConfigFolder("../Config");
        }

        $this->_frontController = \SoftUni\FMK\FrontController::getInstance();

        if($this->_router instanceof \SoftUni\FMK\Routers\IRouter){
            $this->_frontController->setRouter($this->_router);
        }elseif($this->_router == 'JsonRPCRouter'){
            //TODO
            $this->_frontController->setRouter(new DefaultRouter());
        }
        elseif($this->_router == 'CLIRouter'){
            $this->_frontController->setRouter(new DefaultRouter());
        }
        else{
            $this->_frontController->setRouter(new DefaultRouter());
        }

        if ($this->_session == null) {
            $sessionInfo = $this->_config->app['session'];
            if ($sessionInfo['auto_start']) {
                if ($sessionInfo['type'] == 'native') {
                    $this->_session = new NativeSession(
                        $sessionInfo['name'],
                        $sessionInfo['lifetime'],
                        $sessionInfo['path'],
                        $sessionInfo['domain'],
                        $sessionInfo['secure']
                    );
                }
            }
        }

        $this->_frontController->dispatch();
    }

    /**
     * @return ISession
     */
    public function getSession()
    {
        return $this->_session;
    }

    public function setSession(ISession $session)
    {
        $this->_session = $session;
    }


    /**
     * @return \SoftUni\FMK\App
     */
    public static function getInstance(){
        if(self::$_instance == null) {
            self::$_instance = new \SoftUni\FMK\App();
        }

        return self::$_instance;
    }

    public function setConfigFolder($path) {
        $this->_config->setConfigFolder($path);
    }

    public function getConfigFolder() {
        return $this->_config->getConfigFolder();
    }

    /**
     * @return Config | null
     */
    public function getConfig() {
        return $this->_config;
    }

    public function getDBConnection($connection = 'default')
    {
        if (!$connection) {
            throw new \Exception('No connection string provided', 500);
        }

        if ($this->_dbConnections[$connection]) {
            return $this->_dbConnections[$connection];
        }

        $dbConfig = $this->getConfig()->database;
        if (!$dbConfig[$connection]) {
            throw new \Exception('No valid connection string found in config file', 500);
        }

        $database = new \PDO(
            $dbConfig[$connection]['connection_uri'],
            $dbConfig[$connection]['username'],
            $dbConfig[$connection]['password'],
            $dbConfig[$connection]['pdo_options']
        );
        $this->_dbConnections[$connection] = $database;
        return $database;
    }

    public function _exceptionHandler(\Exception $ex)
    {
        if ($this->_config && $this->_config->app['displayExceptions'] == true) { //switch to false for production
            echo '<pre>' . print_r($ex, true) . '</pre>';
        } else {
            $this->displayError($ex->getCode());
        }
    }

    public function displayError($error)
    {
        try {
            $view = View::getInstance();
            $view->display('errors' . $error);
            // TODO proper catch!
        } catch (\Exception $ex) {
            //TODO header status
            \SoftUni\FMK\Common::headerStatus($error);
            echo '<h1>' . $error . '</h1>';
            exit;
        }
    }

    public function __destruct()
    {
        if ($this->_session != null) {
            $this->_session->saveSession();
        }
    }

}