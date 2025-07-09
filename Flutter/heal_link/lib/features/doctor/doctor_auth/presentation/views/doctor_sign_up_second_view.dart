import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_background_gradient_container.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_second_sign_up_body.dart';

class DoctorSignUpSecondView extends StatelessWidget {
  const DoctorSignUpSecondView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: AuthGradiantBackGroundContainer(widget: DoctorSignUpSecondBody()),
    );
  }
}
