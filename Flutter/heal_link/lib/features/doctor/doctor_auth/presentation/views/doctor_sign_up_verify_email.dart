import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_background_gradient_container.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_up_verify_email_body.dart';

class DoctorSignUpVerifyEmail extends StatelessWidget {
  const DoctorSignUpVerifyEmail({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: AuthGradiantBackGroundContainer(widget: DoctorSignUpVerifyEmailBody()),
    );
  }
}
