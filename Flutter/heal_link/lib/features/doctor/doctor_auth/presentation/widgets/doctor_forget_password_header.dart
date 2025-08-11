
import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_auth_header.dart';

class DoctorForgetPasswordHeader extends StatelessWidget {
  const DoctorForgetPasswordHeader({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: CustomAuthHeader(headerTitle: "Forget Password"),
    );
  }
}
