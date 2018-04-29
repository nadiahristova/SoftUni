<?php
namespace SoftUni\FMK;

class View{

    private static $_instance = null;
    private $___viewPath = null;
    private $___viewDir = null;
    private $___data = [];
    private $___layoutParts = [];
    private $___layoutData = [];
    private $___extension = '.php';

    private function __construct()
    {
        $this->___viewPath = App::getInstance()->getConfig()->app['viewsDirectory'];
        if ($this->___viewPath == null) {
            $this->___viewPath = realpath('../Views/');
        }

    }

    public static function getInstance()
    {
        if (self::$_instance == null) {
            self::$_instance = new View();
        }

        return self::$_instance;
    }

    public function __get($name)
    {
        return $this->___data[$name];
    }

    public function __set($name, $value)
    {
        $this->___data[$name] = $value;
    }

    public function setViewDirectory($path)
    {
        $path = trim($path);
        if ($path) {
            $path = realpath($path) . DIRECTORY_SEPARATOR;
            if (is_dir($path) && is_readable($path)) {
                $this->___viewDir = $path;
            } else {
                throw new \Exception('Problem with view path', 500);
            }
        } else {
            throw new \Exception('Problem with view path', 500);
        }
    }

    public function display($name, $data= [], $returnAsString  = false)
    {
        if(is_array($data)){
            $this->data = array_merge($this->___data, $data);
        }

        if(count($this->___layoutParts) > 0){
            foreach($this->___layoutParts as $key => $value){
                $r = $this->_includeFile($value);
                if($r){
                    $this->___layoutData[$key] = $r;
                }
            }
        }
//        $this->ValidateViewModel($viewModel);
//        $this->_viewBag = $viewModel;
//        $this->_includeFile($viewModel);
//        $file = $this->GetViewModelPath($viewModel);
//        $path = str_replace('.', DIRECTORY_SEPARATOR, $file);
//        $fullPath = $this->___viewDir . $path . $this->___extension;
//        $this->_includeView($fullPath);

        if($returnAsString){
            return $this->_includeFile($name);
        } else {
            echo $this->_includeFile($name);
        }
    }

    private function _includeFile($file)
    {
        if ($this->___viewDir == null) {
            $this->setViewDirectory($this->___viewPath);
        }
//
//        if (!is_string($file)) {
//            $file = $this->GetViewModelPath($file);
//        }

        $___fl = $this->___viewDir . str_replace('.', DIRECTORY_SEPARATOR, $file) . $this->___extension;
        if (file_exists($___fl) && is_readable($___fl)) {

            // adds to different buffer
            ob_start();
            //$this->includeView($fullPath);
            include $___fl;
            // returns the buffer as string
            return ob_get_clean();
        } else {
            throw new \Exception('View ' . $file . ' cannot be included', 500);
        }
    }

    public function appendToLayout($key, $template)
    {
        if ($key && $template) {
            if (!is_string($template)) {
                //$this->ValidateViewModel($template);
                $this->___data[$key] = $template;
            }

            $this->___layoutParts[$key] = $template;
        } else {
            throw new \Exception('Layouts require valid key and template!', 500);
        }
    }

    public function getLayoutData($name)
    {
        return $this->___layoutData[$name];
    }
}