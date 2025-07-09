import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_logo.dart';
import 'package:heal_link/core/widgets/custom_sliver_height_16.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_in_divider.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_in_footer.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_in_form.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_in_header.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_in_social_buttons.dart';

class DoctorSignInBody extends StatelessWidget {
  const DoctorSignInBody({super.key});

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16),
        child: CustomScrollView(
          slivers: [
            DoctorSignInHeader(),
            CustomSliverHeight(32),
            CustomAppLogo(),
            CustomSliverHeight(32),
            DoctorSignInForm(),
            CustomSliverHeight(16),
            DoctorSignInRowDivider(),
            DoctorSignInSocialMediaButtons(),
            DoctorSignInFooter(),
          ],
        ),
      ),
    );
  }
}

