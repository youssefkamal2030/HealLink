import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_logo.dart';
import 'package:heal_link/core/widgets/custom_sliver_height_16.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_forget_pass_message.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_forget_password_form.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_forget_password_header.dart';

class DoctorForgetPasswordBody extends StatelessWidget {
  const DoctorForgetPasswordBody({super.key});

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: EdgeInsetsGeometry.symmetric(horizontal: 16),

        child: CustomScrollView(
          slivers: [
            DoctorForgetPasswordHeader(),
            CustomSliverHeight(32),
            CustomAppLogo(),
            CustomSliverHeight(32),
            DoctorForgetPassswordPleaseMessage(),
            CustomSliverHeight(24),
            DoctorForgetPasswordForm(),
          ],
        ),
      ),
    );
  }
}
