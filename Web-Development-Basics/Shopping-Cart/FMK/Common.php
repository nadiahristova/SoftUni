<?php

namespace SoftUni\FMK;


class Common {

    public static function normalize($data, $types){
        $types = explode('|', $types);
        if(is_array($types)){
            foreach($types as $type){
                switch($type){
                    case 'int' : $data = (int)$data; break;
                    case 'float' : $data = (float)$data; break;
                    case 'double' : $data = (double)$data; break;
                    case 'bool' : $data = (bool)$data; break;
                    case 'string' : $data = (string)$data; break;
                    case 'trim' : $data = trim($data); break;
                    default: throw new \Exception("Unsypported normalize type.", 500); break;
                }
            }
        }

        return $data;
    }

    public static function headerStatus($code){
        $statuses = array(
            100 => 'Continue',
            101 => 'Switching Protocols',
            200 => 'OK',
            201 => 'Created',
            202 => 'Accepted',
            203 => 'Non-Authoritative Information',
            204 => 'No Content',
            205 => 'Reset Content',
            206 => 'Partial Content',
            207 => 'Multi-Status',
            300 => 'Multiple Choices',
            301 => 'Moved Permanently',
            302 => 'Found',
            303 => 'See Other',
            304 => 'Not Modified',
            305 => 'Use Proxy',
            307 => 'Temporary Redirect',
            400 => 'Bad Request',
            401 => 'Unauthorized',
            402 => 'Payment Required',
            403 => 'Forbidden',
            404 => 'Not Found',
            405 => 'Method Not Allowed',
            406 => 'Not Acceptable',
            407 => 'Proxy Authentication Required',
            408 => 'Request Timeout',
            409 => 'Conflict',
            410 => 'Gone',
            411 => 'Length Required',
            412 => 'Precondition Failed',
            413 => 'Request Entity Too Large',
            414 => 'Request-URI Too Long',
            415 => 'Unsupported Media Type',
            416 => 'Requested Range Not Satisfiable',
            417 => 'Expectation Failed',
            422 => 'Unprocessable Entity',
            423 => 'Locked',
            424 => 'Failed Dependency',
            500 => 'Internal Server Error',
            501 => 'Not Implemented',
            502 => 'Bad Gateway',
            503 => 'Service Unavailable',
            504 => 'Gateway Timeout',
            505 => 'HTTP Version Not Supported',
            507 => 'Insufficient Storage',
            509 => 'Bandwidth Limit Exceeded'
        );

        if(!$statuses[$code]){
            $code = 500;
        }

        header($_SERVER['SERVER_PROTOCOL'] . ' ' . $statuses[$code], true, $code);
    }
}