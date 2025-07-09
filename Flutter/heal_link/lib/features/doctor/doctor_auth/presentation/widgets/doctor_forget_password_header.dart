
import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_custom_header.dart';

class DoctorForgetPasswordHeader extends StatelessWidget {
  const DoctorForgetPasswordHeader({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: AuthCustomHeader(headerTitle: "Forget Password"),
    );
  }
}
