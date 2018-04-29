<?php
/**
 * Created by PhpStorm.
 * User: Nadia
 * Date: 14.11.2015 г.
 * Time: 21:12 ч.
 */

namespace PFCS\FMK\Annotation;


class Route extends Annotation
{
    public $pattern;
    public $name;

    public function usage() : string
    {
        return "Maps a route pattern with a controller action.";
    }

    public function isFor() : int
    {
        return $this::FOR_METHOD | $this::FOR_CLASS;
    }
}