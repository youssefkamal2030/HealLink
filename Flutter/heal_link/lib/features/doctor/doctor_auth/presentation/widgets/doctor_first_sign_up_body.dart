import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_logo.dart';
import 'package:heal_link/core/widgets/custom_sliver_height_16.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_up_footer.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_first_sign_up_form.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_up_header.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_up_row_divider.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_up_social_media_row.dart';

class DoctorFirstSignUpBody extends StatefulWidget {
  const DoctorFirstSignUpBody({super.key});

  @override
  State<DoctorFirstSignUpBody> createState() => _DoctorFirstSignUpBodyState();
}

class _DoctorFirstSignUpBodyState extends State<DoctorFirstSignUpBody> {
  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16),
        child: CustomScrollView(
          slivers: [
            DoctorSignUpHeader(),
            CustomSliverHeight(16),
            CustomAppLogo(),
            CustomSliverHeight(16),
            DoctorFirstSignupForm(),
            CustomSliverHeight(16),
            DoctorSignUpRowDivider(),
            CustomSliverHeight(16),
            DoctorSignUpSocialMediaButtonsRow(),
            CustomSliverHeight(16),
            DoctorSignUpFooter(),
            CustomSliverHeight(16),
          ],
        ),
      ),
    );
  }
}
