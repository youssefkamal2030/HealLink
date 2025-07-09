import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_background_gradient_container.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_first_sign_up_body.dart';

class DoctorSignUpFirstView extends StatelessWidget {
  const DoctorSignUpFirstView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: AuthGradiantBackGroundContainer(
        widget: DoctorFirstSignUpBody(),
      ),
    );
  }
}
