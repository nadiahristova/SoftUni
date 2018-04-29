<?php

namespace PFCS\FMK;


use PFCS\FMK\Routers\IRouter;

class FrontController
{
    /**
     * @var FrontController
     */
    private static $_instance = null;
    private $namespace = null;
    private $controller = null;
    private $method = null;

    /**
     * @var IRouter
     */
    private $router = null;

    /**
     * @var Config
     */
    private $config = null;

    public function getRouter() : IRouter
    {
        return $this->router;
    }

    public function setRouter(IRouter $router)
    {
        $this->router = $router;
    }

    private function __construct()
    {
        $this->config = App::getInstance()->getConfig();
    }

    public function dispatch()
    {
        if ($this->router == null) {
            throw new \Exception('No valid router found.', 500);
        }
        $uri = $this->router->getURI();
        $input = InputData::getInstance();

        if ($this->parseCustomRoute($input, $uri)) {
            return;
        }

        $params = explode('/', $uri);
        $this->controller = array_shift($params);
        if (!$this->controller) {
            $this->controller = $this->getDefaultController();
            $this->method = $this->getDefaultMethod();
        } else {
            $this->method = array_shift($params);
            if (!$this->method) {
                $this->method = $this->getDefaultMethod();
            }
            $input->setGet($params);
        }
        $this->namespace = $this->config->app['defaultControllerNamespace'];

        $input->setPost($this->router->getPost());
        $this->loadControllerAction();
    }

    public function getDefaultController() : string
    {
        $controller = $this->config->app['default_controller'];
        if ($controller) {
            return strtolower($controller);
        }

        return 'index';
    }

    public function getDefaultMethod() : string
    {
        $method = $this->config->app['default_method'];
        if ($method) {
            return strtolower($method);
        }

        return 'index';
    }

    public static function getInstance() : FrontController
    {
        if (self::$_instance == null) {
            self::$_instance = new  FrontController();
        }

        return self::$_instance;
    }

    public function loadControllerAction()
    {
        $controllerPrefix = $this->controller;
        $this->controller = $this->controller . 'Controller';
        $namespaceClass = $this->namespace . '\\' . ucfirst($this->controller);

        $isCallable = false;
        try {
            $isCallable = is_callable(array($namespaceClass, $this->method));
        } catch (\Exception $ex) {
            throw new \Exception('Not found', 404);
        }

        if ($isCallable) {
            $bindingModel = null;
            if ($this->router->getPost()) {//TODO post binding models to be changed
                $bindingModel = BindingModel::validate($this->router->getPost(),
                    $this->config->bindingModels[$this->namespace][$controllerPrefix][strtolower($this->method)]);
            }

            $newController = new $namespaceClass();
            call_user_func(array($newController, $this->method), $bindingModel);
        } else {
            throw new \Exception('Not found', 404);
        }
    }

    public function parseCustomRoute(InputData $input, string $uri)//TODO change
    {
        $customRoutes = $this->config->routes;
        if ($customRoutes && is_array($customRoutes)) {
            foreach ($customRoutes as $customRoute => $controllerAction) {
                $routeParts = explode('/{', $customRoute);
                $matchedCustomRoute = array_shift($routeParts);
                $hasParams = preg_match_all('/{(.+?)}/i', $customRoute, $result);
                $routeParamNames = $result[1];

                if (stripos($uri, $matchedCustomRoute) === 0
                    && ($uri == $matchedCustomRoute || stripos($uri, $matchedCustomRoute . '/') === 0)
                ) {

                    if ($hasParams) {
                        $paramsUri = substr($uri, strlen($matchedCustomRoute) + 1);
                        $paramValues = explode('/', $paramsUri);
                        $params = array();
                        foreach ($routeParamNames as $paramName) {
                            $params[$paramName] = array_shift($paramValues);
                        }
                    }


                    if ($hasParams) {
                        $matchedCustomRoute = substr($matchedCustomRoute, strlen($customRoute) + 1);
                    } else {
                        $matchedCustomRoute = substr($uri, strlen($customRoute) + 1);
                    }

                    $matchedCustomRouteArray = explode('/', $matchedCustomRoute);

                    if ($controllerAction[1]) {
                        $this->controller = $controllerAction[1];
                    } else {
                        $this->controller = array_shift($matchedCustomRouteArray);
                        if (!$this->controller) {
                            $this->controller = $this->getDefaultController();
                        }
                    }
                    if ($controllerAction[2]) {
                        $this->method = $controllerAction[2];
                    } else {
                        $this->method = array_shift($matchedCustomRouteArray);
                        if (!$this->method) {
                            $this->method = $this->getDefaultMethod();
                        }
                    }

                    if ($hasParams) {
                        $input->setGet(array_merge($params, $matchedCustomRouteArray));
                    } else {
                        $input->setGet($matchedCustomRouteArray);
                    }
                    $input->setPost($this->router->getPost());
                    $this->namespace = $controllerAction[0] ? $controllerAction[0] : $this->config->app['defaultControllerNamespace'];
                    $this->loadControllerAction();
                    return true;
                }
            }
        }
    }
}