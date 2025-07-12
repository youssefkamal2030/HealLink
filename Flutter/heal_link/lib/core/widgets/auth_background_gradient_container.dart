
import 'package:flutter/material.dart';

class AuthGradiantBackGroundContainer extends StatelessWidget {
  const AuthGradiantBackGroundContainer({
    super.key,
    required this.widget , 
  });
final Widget widget ; 
  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      height: double.infinity,
      decoration: BoxDecoration(
        gradient: LinearGradient(
          begin: Alignment.topRight,
          end: Alignment.center,
          colors: [Color(0xffADC5F5), Color(0xffF4F6F8)],
        ),
      ),
      child: widget,
    );
  }
}
