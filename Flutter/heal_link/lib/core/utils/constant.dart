import 'package:flutter/cupertino.dart';

abstract class AppConstant{
 static late double height;
  static late double width;

  static getSizes(context){
    height=MediaQuery.of(context).size.height;
    width=MediaQuery.of(context).size.width;
  }
}