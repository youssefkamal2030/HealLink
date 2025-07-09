import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_custom_header.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignUpHeader extends StatelessWidget {
  const DoctorSignUpHeader({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: AuthCustomHeader(headerTitle: S.of(context).sign_up),
    );
  }
}
