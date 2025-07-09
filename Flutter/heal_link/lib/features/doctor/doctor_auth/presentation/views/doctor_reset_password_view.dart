import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_background_gradient_container.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_resert_password_body.dart';

class DoctorResetPasswordView extends StatelessWidget {
  const DoctorResetPasswordView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(body: AuthGradiantBackGroundContainer(widget: DoctorResetPasswordBody()));
  }
}
