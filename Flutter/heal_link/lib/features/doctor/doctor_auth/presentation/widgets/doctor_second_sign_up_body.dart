import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_logo.dart';
import 'package:heal_link/core/widgets/custom_sliver_height_16.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_up_header.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_second_sign_up_form.dart';

class DoctorSignUpSecondBody extends StatelessWidget {
  const DoctorSignUpSecondBody({super.key});

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: EdgeInsets.symmetric(horizontal: 16),
        child: CustomScrollView(
          slivers: [
            DoctorSignUpHeader(),
            CustomSliverHeight(32),
             CustomAppLogo(),
            DoctorSignUpSecondForm(),
          ],
        ),
      ),
    );
  }
}
