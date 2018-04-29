<?php

namespace SoftUni\FMK;

use SoftUni\FMK\Routers\DefaultRouter;

class FrontController {
    private static $_instance = null;
    private $namespace = null;
    private $controller = null;
    private $method = null;
    /** @var \SoftUni\FMK\Routers\IRouter */
    private $router = null;

    /**
     * @return \SoftUni\FMK\Routers\IRouter | null
     */
    public function getRouter()
    {
        return $this->router;
    }

    /**
     * @param \SoftUni\FMK\Routers\IRouter $router | null
     */
    public function setRouter(\SoftUni\FMK\Routers\IRouter $router)
    {
        $this->router = $router;
    }

    private function __construct() {

    }


    public function dispatch()
    {
        if($this->router == null){
            throw new \Exception('No valid router found', 500);
        }

        $_uri = $this->router->getURI();
        $routes = \SoftUni\FMK\App::getInstance()->getConfig()->routes;
        $_routeController = null;
        if(is_array($routes) && count($routes) > 0) {
            foreach($routes as $key => $value) {
                if(stripos($_uri, $key) === 0 && ($_uri == $key || stripos($_uri, $key . '/') === 0) && $value['namespace']) {
                    $this->namespace = $value['namespace'];
                    $_uri = substr($_uri, strlen($key) + 1);
                    $_routeController = $value;
                    break;
                }
            }
        }else {
            throw new \Exception('Default route missing.', 500);
        }

        if($this->namespace == null && $routes['*']['namespace']) {
            $this->namespace = $routes['*']['namespace'];
            $_routeController = $routes['*'];
        } elseif($this->namespace == null &&  !$routes['*']['namespace']) {
            throw new \Exception('Default route missing', 500);
        }

        $input = \SoftUni\FMK\InputData::getInstance();
        $_params = explode('/', $_uri);
        if($_params[0]) {
            $this->controller = strtolower(array_shift($_params));
            if($_params[0]) {
                $this->method = strtolower(array_shift($_params));
                $input->setGet(array_values($_params));
            }else {
                $this->method = $this->getDefaultMethod();
            }
        } else {
            $this->controller = $this->getDefaultController();
            $this->method = $this->getDefaultMethod();
        }

        if(is_array($_routeController) && $_routeController['controllers']) {
            if($_routeController['controllers'][$this->controller]['methods'][$this->method]){
                $this->method = strtolower($_routeController['controllers'][$this->controller]['methods'][$this
                    ->method]);
            }
            if(isset($_routeController['controllers'][$this->controller]['to'])){
                $this->controller = strtolower($_routeController['controllers'][$this->controller]['to']);
            }
        }

        $input->setPost($this->router->getPost());

        $f = $this->namespace . '\\' . ucfirst($this->controller);
        $newController = new $f();
        $newController->{$this->method}();
    }

    public function getDefaultController() {
        $app = \SoftUni\FMK\App::getInstance();
        $controller = isset($app->getConfig()->app['default_controller']) ? $app->getConfig()->app['default_controller'] : null;
        if($controller) {
            return strtolower($controller);
        }

        return 'index';
    }
    public function getDefaultMethod() {
        $app = \SoftUni\FMK\App::getInstance();
        $method = isset($app->getConfig()->app['default_method']) ? $app->getConfig()->app['default_method'] : null;
        if($method) {
            return strtolower($method);
        }

        return 'index';
    }

    /**
     * @return \SoftUni\FMK\FrontController
     */
    public static function getInstance() {
        if(self::$_instance == null) {
            self::$_instance = new \SoftUni\FMK\FrontController();
        }

        return self::$_instance;
    }
}