import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_background_gradient_container.dart';
// ignore: unused_import
import 'package:heal_link/core/widgets/auth_custom_header.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_forget_password_body.dart';

class DoctorForgetPasswordView extends StatelessWidget {
  const DoctorForgetPasswordView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: AuthGradiantBackGroundContainer(widget: DoctorForgetPasswordBody()),
    );
  }
}
