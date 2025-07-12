
import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_custom_header.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignInHeader extends StatelessWidget {
  const DoctorSignInHeader({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: AuthCustomHeader(headerTitle: S.of(context).sign_in , ),
    );
  }
}